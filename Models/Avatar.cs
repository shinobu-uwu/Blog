using System.Net.Mime;

namespace Blog.Models;

public class Avatar : BaseModel
{
    public string ContentType { get; set; }
    public byte[] Data { get; set; }

    public Avatar()
    {
        CreationDate = DateTime.Now;
        Enabled = true;
    }

    public Avatar(IFormFile file)
    {
        var stream = file.OpenReadStream();
        var br = new BinaryReader(stream);

        CreationDate = DateTime.Now;
        Enabled = true;
        Data = br.ReadBytes((int)file.Length);
        ContentType = file.ContentType;
    }
}