namespace TopBookStore.Mvc.Models.Chart
{
    public class ChildDrillDownViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerable<List<Object>> data { get; set; }
    }
}
