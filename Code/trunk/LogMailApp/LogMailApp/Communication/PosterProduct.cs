
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

        public void Run(UserData userData)
        {
            try
            {
                this.Poster.Post(userData);
            }
            catch (System.Exception)
            {
                if (this.Poster.WillStopOnError)
                    throw;
            }

            if (this.Next != null)
            {
                this.Next.Run(userData);
            }
        }
    }
}
