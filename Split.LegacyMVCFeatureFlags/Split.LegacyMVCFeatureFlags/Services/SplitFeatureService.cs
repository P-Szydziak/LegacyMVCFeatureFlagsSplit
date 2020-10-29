using Splitio.Services.Client.Interfaces;

namespace Split.LegacyMVCFeatureFlags.Services
{
    public enum SplitFeatureFlags
    {
        LegacyProductBlogRef
    }
    public class SplitFeatureService
    {
        private readonly ISplitFactory _splitFactory;
        public SplitFeatureService(ISplitFactory splitFactory)
        {
            _splitFactory = splitFactory;
        }

        public bool ValidFlag(SplitFeatureFlags flag, string user)
        {
            var splitClient = _splitFactory.Client();

            try
            {
                splitClient.BlockUntilReady(10000);
            }
            catch
            {
                return false;
            }
            var treatment = splitClient.GetTreatment(user, flag.ToString());
            return treatment == "on";
        }
    }
}