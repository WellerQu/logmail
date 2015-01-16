
namespace LogMailApp.Communication
{
    class PosterProduct
    {
        protected PosterProduct()
        {
        }

        public PosterProduct(IPoster poster)
            : this()
        {
            this.Poster = poster;
        }

        public PosterProduct Next { set; get; }
        private IPoster Poster { get; set; }

        public void Run(UserData userData, ref string error)
        {
            try
            {
                this.Poster.Ready(userData);
                this.Poster.Post(userData);
            }
            catch (System.Exception ex)
            {
                if (this.Poster.WillStopOnError)
                    throw ex;
                else
                    error += ex.Message + ";";
            }

            if (this.Next != null)
            {
                this.Next.Run(userData, ref error);
            }
        }
    }
}
