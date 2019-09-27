using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apibackend.Trasversal.DTOs
{
    public class UserDetail
    {
        public UserDetail() {
            photo = new DTOs.ProfilePhoto();
        }
        ~UserDetail() { }        
        public string aboutMe { get; set; }
        public bool accountEnabled { get; set; }
        //public List<AssignedLicens> assignedLicenses { get; set; }
        //public List<AssignedPlan> assignedPlans { get; set; }
        public string birthday { get; set; }
        public List<string> businessPhones { get; set; }
        public string city { get; set; }
        public string companyName { get; set; }
        public string country { get; set; }
        public string department { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string hireDate { get; set; }
        public string id { get; set; }
        public List<string> interests { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        //public MailboxSettings mailboxSettings { get; set; }
        public string mailNickname { get; set; }
        public string mobilePhone { get; set; }
        public string mySite { get; set; }
        public string officeLocation { get; set; }
        public string onPremisesImmutableId { get; set; }
        public string onPremisesLastSyncDateTime { get; set; }
        public string onPremisesSecurityIdentifier { get; set; }
        public bool onPremisesSyncEnabled { get; set; }
        public string passwordPolicies { get; set; }
        //public PasswordProfile passwordProfile { get; set; }
        public List<string> pastProjects { get; set; }
        public string postalCode { get; set; }
        public string preferredLanguage { get; set; }
        public string preferredName { get; set; }
        //public List<ProvisionedPlan> provisionedPlans { get; set; }
        public List<string> proxyAddresses { get; set; }
        public List<string> responsibilities { get; set; }
        public List<string> schools { get; set; }
        public List<string> skills { get; set; }
        public string state { get; set; }
        public string streetAddress { get; set; }
        public string surname { get; set; }
        public string usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
        //public Calendar calendar { get; set; }
        //public List<CalendarGroup> calendarGroups { get; set; }
        //public List<CalendarView> calendarView { get; set; }
        //public List<Calendar2> calendars { get; set; }
        //public List<Contact> contacts { get; set; }
        //public List<ContactFolder> contactFolders { get; set; }
        //public List<CreatedObject> createdObjects { get; set; }
        //public List<DirectReport> directReports { get; set; }
        //public Drive drive { get; set; }
        //public List<Drive2> drives { get; set; }
        //public List<Event> events { get; set; }
        //public InferenceClassification inferenceClassification { get; set; }
        //public List<MailFolder> mailFolders { get; set; }
        //public Manager manager { get; set; }
        //public List<MemberOf> memberOf { get; set; }
        //public List<Message> messages { get; set; }
        //public List<OwnedDevice> ownedDevices { get; set; }
        //public List<OwnedObject> ownedObjects { get; set; }
        public Apibackend.Trasversal.DTOs.ProfilePhoto photo { get; set; }
        //public List<RegisteredDevice> registeredDevices { get; set; }

    }
}
