using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace BuisnessLogic.Blob;
public class BlobClass
{
    BlobServiceClient _blobClient;
    BlobContainerClient _containerClient;
    string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=restaurantstorage;AccountKey=yD/cQeN44O3SUPYCViHX8dNlKaQakwrL0NBSgtz/iClga2IhKvbrWbiqc8RFbwcQw8vV/qem2ke1+AStoHg6xw==;EndpointSuffix=core.windows.net";
    public BlobClass()
    {

        _blobClient = new BlobServiceClient(azureConnectionString);
        _containerClient = _blobClient.GetBlobContainerClient("dishesimage");

    }

    public async Task<Azure.Response<BlobContentInfo>> UploadFiles(IFormFile file)
    {
        string fileName = file.FileName;
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;


        var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);

        //var l = client.GetRawResponse;
        return client;


    }

}
