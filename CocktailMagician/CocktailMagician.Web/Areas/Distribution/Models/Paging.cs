using System;

namespace CocktailMagician.Web.Areas.Distribution.Models
{
    public class Paging
    {
        public int Count { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage
        {
            get
            {
                return (int)Math.Ceiling(Count/(double)ItemsPerPage);
            }
        }
        public bool HasPrevious
        {
            get
            {
                return (this.CurrentPage > 1);
            }
        }
        public bool HasNext
        {
            get
            {
                return (this.CurrentPage < this.LastPage);
            }
        }
        public int PreviousPage
        {
            get
            {
                if(this.CurrentPage == 1)
                {
                    return 1;
                }
                return this.CurrentPage - 1;
            }
        }
        public int NextPage
        {
            get
            {
                if (this.CurrentPage == this.LastPage)
                {
                    return this.LastPage;
                }
                return this.CurrentPage + 1;
            }
        }

    }
}
