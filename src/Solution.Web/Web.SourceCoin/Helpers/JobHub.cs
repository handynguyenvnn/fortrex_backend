using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web.SourceCoin.Helpers
{
    [HubName("jobHub")]
    public class JobHub : Hub
    {
        private readonly JobInfoRepository _jobRepository;
        public JobHub() : this(JobInfoRepository.Instance) { }
        public JobHub(JobInfoRepository jobRepository)
        {
            _jobRepository = jobRepository;

        }
        public void setUid(string uid = "")
        {
            _jobRepository.userid = uid;
        }
        //public void setPairname(string pair = "")
        //{
        //    _jobRepository.pair = pair;
        //}
        //public IEnumerable<HighchartSyncTrade> ShowData()
        //{
        //    return _jobRepository.ShowData();
        //}
        //public void getTime_By_CountryZone(string countryZone)
        //{
        //    TimeZone currentZone = TimeZone.CurrentTimeZone;
        //    DateTime currentDate = DateTime.Now;
        //    DateTime currentUTC = currentZone.ToUniversalTime(currentDate);
        //    TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(countryZone);
        //    DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentUTC, selectedTimeZone);
        //    Clients.Caller.setTime(currentDateTime.ToString("ss"));
        //}
        public void serverGetTime()
        {
            //Clients.All.serverSetSecond(DateTime.Now.Second);
            Clients.All.serverSetSecond(_jobRepository.ServerTime());
        }


    }
}