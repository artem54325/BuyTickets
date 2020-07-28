using ClickLikeVk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingTicketCore.Helpers
{
    public class PutData
    {
        public List<LikeVkModel> models = new List<LikeVkModel>();

        public PutData()
        {
            models.Add(new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = "1",
                DateTime = DateTime.Now
            });
            models.Add(new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = "2",
                DateTime = DateTime.Now
            });
            models.Add(new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = "3",
                DateTime = DateTime.Now
            });
            models.Add(new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = "2",
                DateTime = DateTime.Now
            });
            models.Add(new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = "1",
                DateTime = DateTime.Now
            });
        }
    }
}
