using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Model.DTO
{
    public class ExtendedSiteDTO
    {
        public string SiteName { get; set; }
        public string OwnerName { get; set; }

        /// <summary>
        /// A dictionary contains key value pairs, the key will be the rolling stock serial number,
        /// and the value will be a SiteData object
        /// </summary>
        public Dictionary<string, SiteData> SiteDatas { get; set; }

        public ExtendedSiteDTO(string siteName, string ownerName)
        {
            SiteName = siteName;
            OwnerName = ownerName;
            SiteDatas = new Dictionary<string, SiteData>();
        }
    }
}
