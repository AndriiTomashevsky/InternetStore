using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetStore.Models.ViewModels
{
    public class SortingInfo
    {
        public SortState? CurrentSortState { get; set; }
        public SortState NameSort { get; set; }
        public SortState PriceSort { get; set; }

        public SortingInfo(SortState? sortState)
        {
            CurrentSortState = sortState;
            NameSort = SortState.NameAsc;
            PriceSort = SortState.PriceAsc;

            switch (sortState)
            {
                case SortState.NameAsc:
                    NameSort = SortState.NameDesc;
                    break;
                case SortState.NameDesc:
                    NameSort = SortState.NameAsc;
                    break;
                case SortState.PriceAsc:
                    PriceSort = SortState.PriceDesc;
                    break;
                case SortState.PriceDesc:
                    PriceSort = SortState.PriceAsc;
                    break;
            }
        }
    }
}
