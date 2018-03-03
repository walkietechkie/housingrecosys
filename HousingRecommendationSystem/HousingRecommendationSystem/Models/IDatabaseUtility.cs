using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingRecommendationSystem.Models
{
    public interface IDatabaseUtility
    {
        IEnumerable<PropertyModel> GetPropertyModelByBucketId(string bucketId);
    }
}
