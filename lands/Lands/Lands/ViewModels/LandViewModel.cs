namespace Lands.ViewModels
{
    using Models;
    public class LandViewModel
    {
        #region Properties
        public Land Land
        {
            get;
            set;
        }
        #endregion

        #region COnstructors
        public LandViewModel(Land land)
        {
            this.Land = land;
        }
        #endregion
    }
}
