using PeopleSite.Data;
using System.Globalization;

namespace PeopleSite.web.Models
{
    public class IndexViewModel
    {
        public List<Person> People { get; set; }
        public String Message { get; set; }
    }
}
