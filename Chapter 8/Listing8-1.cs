using Google.Cloud.Vision.V1; 
async Task<bool> ImageContentIsSafe(Stream bytes) 
{ 
    Image image = Image.FromStream(bytes); 
    var clientBuilder = new ImageAnnotatorClientBuilder(); 

    //some metod to get your API credentials 
    clientBuilder.JsonCredentials = await Helpers.GetGoogleVisionCredentialsAsync(); 
    ImageAnnotatorClient client = clientBuilder.Build(); 
    SafeSearchAnnotation annotation = client.DetectSafeSearch(image); 

    // Each category is classified as Very Unlikely, Unlikely, Possible, Likely or Very Likely. 
    return annotation.Adult != Likelihood.VeryLikely //porn? 
    && annotation.Medical < Likelihood.Likely; //surgery, etc?
} 