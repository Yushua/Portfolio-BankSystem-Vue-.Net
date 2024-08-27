namespace MyApp.Models{
        /*
        you can make a private functonn that gets and sets these values when creating to make it more secure.
        actions outside cannot affect the class itself them, only the functions given can be used, and those can be protected.
        for example, so access the function, one has to... have priviliges to use it.
    */

    public class Computer {
        private string motherboard = "";
        private string videoCard = "";
        private int CPUCores = 0;

        private bool HasWifi = false;
        private DateOnly ReleaseDate = new DateOnly();
        private decimal Price = new decimal(0);
        

        public void SetMotherboard(string value)
        {
            motherboard = value;
        }
        public string GetMotherboard()
        {
            return this.motherboard;
        }

        public void SetVideoCard(string value)
        {
            videoCard = value;
        }
        public string GetVideoCard()
        {
            return this.videoCard;
        }
    }
}