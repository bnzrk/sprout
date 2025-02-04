using System.Runtime.Serialization;

namespace Sprout.Web.Contracts
{
    [DataContract]
    public class CardReviewSummaryDto
    {
        [DataMember]
        public int Due { get; set; }
        [DataMember]
        public int New { get; set; }
    }
}
