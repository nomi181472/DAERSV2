namespace DAERS.API.Helpers.Paging
{
    public class UParams
    {
        public int MaxPageSize =100;
        public int PageNumber { get; set; }=1;
        private int pageSize=10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value>MaxPageSize)? MaxPageSize:value; }
        }
        public int UserId { get; set; }
        public string Category { get; set; }
        public int MinAge { get; set; }=18;
        public int MaxAge { get; set; }=99;
        public string OrderBy { get; set; }
        public bool likers { get; set; }=false;
        public bool likees { get; set; }=false;

    }
}