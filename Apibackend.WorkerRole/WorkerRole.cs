using Apibackend.Trasversal.DTOs;
using ApiBackend.Applicacion.ExternalAgent;
using ApiBackend.Application.Core.MSGraphAuth.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ApiBackend.Transversal.DTOs.PLC;
using ApiBackend.Applicacion.ExternalAgent.BlobStorageManager;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Diagnostics.Management;
using Microsoft.WindowsAzure.Diagnostics;

namespace WRColaTokens
{
    public class WorkerRole : RoleEntryPoint
    {
        // Nombre de la cola
        const string QueueName = "colatokens";

        private ExternalAgent _ea;
        private BlobManager _blobMng;

        // QueueClient es seguro para subprocesos. Se recomienda almacenarlo en caché 
        // en lugar de crearlo de nuevo con cada solicitud
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Iniciando el procesamiento de mensajes");

            // Inicia el bombeo de mensajes y se invoca una devolución de llamada para cada mensaje que se recibe. Si se llama a close en el cliente, se detendrá el bombeo.
            Client.OnMessage(async (receivedMessage) =>
                {
                    try
                    {
                        Stopwatch timer = new Stopwatch();
                        int faceTransaction = 0;
                        int minute = 60000;
                        int liteMinute = 55000;
                        int timeToSleep = 60000;
                        // Procesar el mensaje
                        Trace.WriteLine("Procesando el mensaje de Service Bus: " + receivedMessage.SequenceNumber.ToString());

                        Stream pepa = receivedMessage.GetBody<Stream>();
                        StreamReader reader = new StreamReader(pepa);
                        string s = reader.ReadToEnd();

                        //Obtenemos el token enviado en el mensaje
                        ApiBackend.Transversal.DTOs.PLC.BrokeredMessage message = JsonConvert.DeserializeObject<ApiBackend.Transversal.DTOs.PLC.BrokeredMessage>(s);
                        string token = message.idToken;
                        //token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJLVmN1enFBaWRPTHFXU2FvbDd3Z0ZSR0NZbyJ9.eyJhdWQiOiIyMzhlMjQyNy0wNmVhLTQxNWMtOWE2NS0zZDA5NWY2ZTY5NTciLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vZmE5N2JlNTQtYjAzNy00OTFjLTkzYmMtNjExMTFkZGI4N2Q5L3YyLjAiLCJpYXQiOjE1MTE3ODU2NzUsIm5iZiI6MTUxMTc4NTY3NSwiZXhwIjoxNTExNzg5NTc1LCJhaW8iOiJBVFFBeS84R0FBQUE1VWdKQUtiUU8wS09QVlZpOVV3blQ0VDZ5QlczQm12OVNZMy80b3NjaFZjVnRBNlIxUGxYdHkwcE1LYWdFVDkwIiwibmFtZSI6Ikpvc8OpIEpvYXF1w61uIFNhbGd1ZXJvIENhbWFjaG8iLCJvaWQiOiI5ODc0ZTAwZS02YTMyLTQwZGItODE4Mi03NzUzZjQ3MjdiODQiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJqanNhbGd1ZXJvQGF0c2lzdGVtYXMuY29tIiwic3ViIjoiTXJNNmFsUXNoNC1hTXR5VGFIOXRGSExleEM1aWc3TEp3NUxEbnZjNTFvUSIsInRpZCI6ImZhOTdiZTU0LWIwMzctNDkxYy05M2JjLTYxMTExZGRiODdkOSIsInV0aSI6Il8wSFdkR2dwaEUyME9oaWNJWG85QUEiLCJ2ZXIiOiIyLjAifQ.yj3PdNZNZeEZn_RXgX1YIZKi9oG2BaX8Fn5jAyE6UrM7QKX73Q3fWXK6SrYAaawGX3IGnDTENsgMCHFFJXFEEFqEruXvf-BhDnLubNfM0B2FyDM9Oe0C9JHNIO3y9yjNIHcchzQwYxp77riAH-6yPn2qxaWN5vqE0vO9Osqlbm78XWstlcyEToFgphRakguU-m6kXjeTaYLTRW7IO0YB3UBOAN0G90eLeYpx55TLrlnmC5iAB06dGY1pGyLrIPMdp5-nFUj3vhkzL3jpA9zCJMosm-dAlp0G-4uB8bMjh5dqnPU1zbq4wew8JvcvocXZdndet830EJ46PkLaI49CMw";
                        DateTime inicioToken = DateTime.Now;
                        AuthResult ar = await SampleAuthProvider.Instance.GetUserAccessTokenAsync(token);

                        await _ea.RestartGroup();
                        faceTransaction += 2;
                        if (ar != null)
                        {                                                        
                            List<UserDetail> listadoOffice365 = await GetAllUsersOffice365(ar.AccessToken);
                            for (int i = 0; i < listadoOffice365.Count(); i++)
                            {
                                var photo = await _ea.getUserPhoto(ar.AccessToken, listadoOffice365[i].id);                             
                                if (photo != null)
                                {
                                    byte[] st = new byte[photo.photobytes.Length];
                                    photo.photobytes.Read(st, 0, (int)photo.photobytes.Length);
                                    using (MemoryStream fotoBlob = new MemoryStream(st))
                                    {
                                        photo.photobytes = fotoBlob;
                                        //Almaceno la foto en un blob storage
                                        _blobMng.SaveBlobstorage(photo, listadoOffice365[i].id);
                                    }                                    
                                    photo.photobytes.Dispose();
                                }
                            }
                            timer.Start();
                            var ListaBlobs = _blobMng.GetAllBlobs();
                            //Doy una segunda vuelta para llenar API Face
                            for (int i = 0; i < listadoOffice365.Count(); i++)
                            {
                                int time = (int)timer.ElapsedMilliseconds;
                                if (time >= liteMinute)
                                {                                                                             
                                    Thread.Sleep(timeToSleep);
                                    timer.Reset();
                                    faceTransaction = 0;
                                    timer.Start();
                                }

                                //Limitación de la Api de Face: 20 transacciones por minuto.
                                if (faceTransaction >= 15)
                                {
                                    int timeTranscurred = (int)timer.ElapsedMilliseconds;

                                    if (timeTranscurred <= minute)
                                    {
                                        Thread.Sleep((minute - timeTranscurred) + 3000);         // 3segundos de margen                                       
                                    }
                                    Thread.Sleep(timeToSleep);
                                    timer.Reset();
                                    faceTransaction = 0;
                                    timer.Start();
                                }

                                //Si tiene foto se sube a API Face. Sino no.
                                if(ListaBlobs.listaIdUsers.Exists(a=>a== listadoOffice365[i].id))
                                {
                                    byte[] photo =new byte[] { };

                                    //Subir la foto a API Face
                                    photo = _blobMng.DownloadBlobStorage(listadoOffice365[i].id);
                                    using (MemoryStream photostream = new MemoryStream(photo))
                                    {
                                        string x = await _ea.CreateNewPersonFace(photostream, listadoOffice365[i].displayName, listadoOffice365[i].id);
                                        if (x != "OK")
                                        {
                                            Trace.TraceError(string.Format("Mensaje de error al insertar en la API FACE: {0}; persona: {1}; Id Office365: {2}", x, listadoOffice365[i].displayName, listadoOffice365[i].id));
                                            faceTransaction += 1; //sumo una extra porque si falla debería haber borrado el usuario, para no dejarlo sin foto
                                        }
                                        faceTransaction += 2;
                                    }// Libera la memoria de photostream                                    
                                }                                
                            }
                            await _ea.Train();
                        }

                    }
                    catch (Exception ex)
                    {
                        // Controlar cualquier excepción específica del procesamiento de mensajes aquí
                        Trace.TraceError(string.Format("Mensaje de error general: {0}; StackTrace: {1}", ex.Message, ex.StackTrace));
                    }
                    finally
                    {
                        //receivedMessage.Complete();
                    }                                       
                }
                );

            CompletedEvent.WaitOne();
        }


        public override bool OnStart()
        {
            // Establecer el número máximo de conexiones concurrentes. 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Crear la cola si no existe aún
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Inicializar la conexión con la cola de Service Bus
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);

            _ea = new ExternalAgent(CloudConfigurationManager.GetSetting("PhotoBlobStorageConnection"),
                CloudConfigurationManager.GetSetting("PhotoBlobStorageContainer"));
            _blobMng = new BlobManager(CloudConfigurationManager.GetSetting("PhotoBlobStorageConnection"),
                CloudConfigurationManager.GetSetting("PhotoBlobStorageContainer"));

            //LaunchDiagnostic();

            return base.OnStart();
        }

        private void LaunchDiagnostic()
        {
            string wadConnectionString = "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue(wadConnectionString));


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            RoleInstanceDiagnosticManager roleInstanceDiagnosticManager = CloudAccountDiagnosticMonitorExtensions.CreateRoleInstanceDiagnosticManager(RoleEnvironment.GetConfigurationSettingValue(wadConnectionString), RoleEnvironment.DeploymentId, RoleEnvironment.CurrentRoleInstance.Role.Name, RoleEnvironment.CurrentRoleInstance.Id);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            DiagnosticMonitorConfiguration config = roleInstanceDiagnosticManager.GetCurrentConfiguration();

            if (config == null)
            {
                config = DiagnosticMonitor.GetDefaultInitialConfiguration();
            }

            var transferTime = 5;

            config.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(transferTime);
            config.Logs.ScheduledTransferLogLevelFilter = Microsoft.WindowsAzure.Diagnostics.LogLevel.Verbose;

            roleInstanceDiagnosticManager.SetCurrentConfiguration(config);
        }

        public override void OnStop()
        {
            // Cerrar la conexión con la cola de Service Bus
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }

        /// <summary>
        /// Devuelve la lista completa de usuarios integrados en Office365 sin paginar
        /// </summary>
        /// <param name="token">Token de seguridad</param>
        /// <returns></returns>
        private async Task<List<UserDetail>> GetAllUsersOffice365(string token)
        {
            List<UserDetail> resultado = new List<UserDetail>();

            PaginatedUserDetail allUsers = await _ea.getAllusers(token);
            string firstToken = allUsers.ODataNextLink;
            string nextToken = firstToken;

            while (nextToken != string.Empty)
            {
                resultado.AddRange(allUsers.value);
                allUsers = await _ea.getAllusers(token, nextToken);
                nextToken = allUsers.ODataNextLink;
                if(nextToken == firstToken)
                { break; }
            }


            return resultado;

        }

        //private List<UserDetail> fakeOffice365()
        //{
        //    List<Stream> streams = new List<Stream>();
        //    string directory = System.IO.Path.GetFullPath("C:\\Fotos\\");
        //    foreach (string imagePath in Directory.GetFiles(directory, "*.jpg"))
        //    {
        //        streams.Add(File.OpenRead(imagePath));
        //    }

        //    List<UserDetail> res = new List<UserDetail>() {
        //        new UserDetail() {id = "30edb800-073a-4ded-b2ea-7cea40b8e55d",
        //                          displayName= "Álvaro Alfredo Fuertes Melcón",
        //                          givenName= "Álvaro Alfredo",
        //                          jobTitle= null,
        //                          mail= "aafuertes@atsistemas.com",
        //                          mobilePhone= null,
        //                          officeLocation = null,
        //                          preferredLanguage= null,
        //                          surname = "Fuertes Melcón",
        //                          userPrincipalName= "aafuertes@atsistemas.com",
        //                          photo = new ProfilePhoto() {
        //                              photobytes= streams[0]
        //                          }
                

        //        },
        //        new UserDetail()
        //        {
        //            id = "ba9630ca-57e4-482a-a915-0359246fcb09",
        //            displayName = "Alejandro Almeida Sánchez",
        //            givenName = "Alejandro",
        //            jobTitle = null,
        //            mail = "aalmeida@atsistemas.com",
        //            mobilePhone = null,
        //            officeLocation = null,
        //            preferredLanguage = null,
        //            surname = "Almeida Sánchez",
        //            userPrincipalName = "aalmeida@atsistemas.com",
        //            photo = new ProfilePhoto()
        //            {
        //                photobytes = streams[1]
        //            }
        //        }
        //    };

        //    return res;
        //}


        
    }
}