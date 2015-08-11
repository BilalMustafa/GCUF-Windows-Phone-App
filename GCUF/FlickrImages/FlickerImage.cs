using GCUF.FlickrImages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flickr_Faisalabad
{
    public class FlickerImage
    {
       
        public Uri ImageSmall { get; set; }
        public Uri ImageLarge { get; set; }
        
        
      
        
        
        public async static Task<List<FlickerImage>> GetFlickerImages(string flickrApiKey)
        {
           
            HttpClient client = new HttpClient();
            //Get Url
            var baseUrl = getBaseUrl(flickrApiKey);

            string flickerResult = await client.GetStringAsync(baseUrl);
            //resultTextBlock.Text = flickerResult;
            FlickerData apiData = JsonConvert.DeserializeObject<FlickerData>(flickerResult);

            List<FlickerImage> Images = new List<FlickerImage>();


            if (apiData.stat == "ok")
            {
                foreach (Photo data in apiData.photos.photo)
                {
                    
                    FlickerImage img = new FlickerImage();

                    //To retrieve one photo from the url;
                    //http://farm{farmid}.staticflicker.com/{server-id}/{id}_{secret}{size}.jpg
                    string photoUrl = "http://farm{0}.staticflickr.com/{1}/{2}_{3}";

                    string baseFlickerUrl = string.Format(photoUrl,
                        data.farm,
                        data.server,
                        data.id,
                        data.secret);
                    img.ImageSmall = new Uri(baseFlickerUrl + "_n.jpg");
                    img.ImageLarge = new Uri(baseFlickerUrl + "_b.jpg");
                    Images.Add(img);





                }

            }
            return Images;
            
           
           
        }
       
        private static string getBaseUrl(string flickrApiKey)
        {
            //Licenses to be placed here
            //https://www.flickr.com/services/api/flickr.photos.licenses.getinfo.html
            /*<license id="0" name="All Rights Reserved" url="" />
             * <license id="7" name="No known copyright restrictions" url="http://flickr.com/commons/usage/" />
             * 

             * <license id="4" name="Attribution License" url="http://creativecommons.org/licenses/by/2.0/" />
               <license id="5" name="Attribution-ShareAlike License" url="http://creativecommons.org/licenses/by-sa/2.0/" />
               <license id="6" name="Attribution-NoDerivs License" url="http://creativecommons.org/licenses/by-nd/2.0/" />
               <license id="7" name="No known copyright restrictions" url="http://flickr.com/commons/usage/" />
             */
            string license = "0,4,5,6,7";
            license = license.Replace(",", "%2C");
            //Flicker ApiKey
            ///
          //https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=0401edc0e96db4b374f30c94c10e9350&user_id=127339950%40N04&text=faisalabad&format=json&nojsoncallback=1&api_sig=e47b4212c07283d547471af5f930559b
           
            //my flickr id
            //127339950@N04
            //faisalabad flickr id
            //54908604@N08

            //Website server Url
            //https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=25b7fb5c323fa75fe2392cea1ee1d902&text=faisalabad&format=json&nojsoncallback=1&api_sig=024c51d5e1ed194dd7e71fb09b1034ef
            string url = "https://api.flickr.com/services/rest/" +
                "?method=flickr.photos.search" +
                "&license={0}" +
                "&api_key={1}" +
                "&user_id=127339950@N04" +
                //"&text=faisalabad" +
                "&format=json" +
                "&nojsoncallback=1";
            var baseUrl = string.Format(url, license, flickrApiKey);
            return baseUrl;

        }


    }
}