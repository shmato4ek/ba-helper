using BAHelper.Common.Services;

namespace BAHelper.BLL.Services.Cache
{
    public class InMemoryCache
    {
        public List<CommunicationPlan> CachedPlan { get; set; }
        public RaciMatrix Raci { get; set; }
        public InMemoryCache() 
        {
            CachedPlan = new List<CommunicationPlan>();
            Raci = new RaciMatrix();
        }
    }
}
