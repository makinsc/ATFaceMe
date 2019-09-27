<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureCloudService1" generation="1" functional="0" release="0" Id="c39156e6-a46a-46d8-9df1-948ce5b3be7d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureCloudService1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/AzureCloudService1/AzureCloudService1Group/LB:WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapCertificate|WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:ida_AppId" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:ida_AppId" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:ida_AppSecret" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:ida_AppSecret" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:ida_Audience" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:ida_Audience" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:ida_GraphScopes" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:ida_GraphScopes" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:ida_RedirectUri" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:ida_RedirectUri" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:PhotoBlobStorageConnection" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:PhotoBlobStorageConnection" />
          </maps>
        </aCS>
        <aCS name="WRColaTokens:PhotoBlobStorageContainer" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokens:PhotoBlobStorageContainer" />
          </maps>
        </aCS>
        <aCS name="WRColaTokensInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureCloudService1/AzureCloudService1Group/MapWRColaTokensInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapWRColaTokens:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapWRColaTokens:ida_AppId" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/ida_AppId" />
          </setting>
        </map>
        <map name="MapWRColaTokens:ida_AppSecret" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/ida_AppSecret" />
          </setting>
        </map>
        <map name="MapWRColaTokens:ida_Audience" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/ida_Audience" />
          </setting>
        </map>
        <map name="MapWRColaTokens:ida_GraphScopes" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/ida_GraphScopes" />
          </setting>
        </map>
        <map name="MapWRColaTokens:ida_RedirectUri" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/ida_RedirectUri" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapWRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapWRColaTokens:PhotoBlobStorageConnection" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/PhotoBlobStorageConnection" />
          </setting>
        </map>
        <map name="MapWRColaTokens:PhotoBlobStorageContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/PhotoBlobStorageContainer" />
          </setting>
        </map>
        <map name="MapWRColaTokensInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokensInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WRColaTokens" generation="1" functional="0" release="0" software="C:\Proyectos\Xamarin\AtFaceMe\Source\APIBackend\ApiBackend\Apibackend.Deployment\csx\Release\roles\WRColaTokens" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/AzureCloudService1/AzureCloudService1Group/SW:WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="ida_AppId" defaultValue="" />
              <aCS name="ida_AppSecret" defaultValue="" />
              <aCS name="ida_Audience" defaultValue="" />
              <aCS name="ida_GraphScopes" defaultValue="" />
              <aCS name="ida_RedirectUri" defaultValue="" />
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="PhotoBlobStorageConnection" defaultValue="" />
              <aCS name="PhotoBlobStorageContainer" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WRColaTokens&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WRColaTokens&quot;&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokensInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokensUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokensFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WRColaTokensUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WRColaTokensFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WRColaTokensInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="b4a55608-8dcc-432d-9a5f-04d800e809dc" ref="Microsoft.RedDog.Contract\ServiceContract\AzureCloudService1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="317d08f8-afee-48c0-ba23-6ccf1dcbcf47" ref="Microsoft.RedDog.Contract\Interface\WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AzureCloudService1/AzureCloudService1Group/WRColaTokens:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>