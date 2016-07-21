using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Core.Model
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
