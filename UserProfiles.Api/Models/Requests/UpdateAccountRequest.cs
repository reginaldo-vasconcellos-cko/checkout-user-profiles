using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfiles.Api.Models.Requests
{
    public class UpdateAccountRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
