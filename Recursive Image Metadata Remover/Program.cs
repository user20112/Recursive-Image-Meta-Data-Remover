using SkiaSharp;

string executableLocation = AppContext.BaseDirectory;
List<string> images = new List<string>();
RecursiveGetAllPngImages(executableLocation, images);
foreach (string imageFile in images)
{
    try
    {
        using SKImage image = SKImage.FromEncodedData(File.ReadAllBytes(imageFile));
        File.WriteAllBytes(imageFile, image.Encode(SKEncodedImageFormat.Png, 100).ToArray()); 
        Console.WriteLine($"Decoded and recoded image file :{imageFile}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Had an issue with the file :{imageFile}\r\n is this a correctly formated png ? \r\n {ex.Message}");
    }
}
Console.WriteLine("Finished all found files, press enter to exit");
Console.ReadLine();



void RecursiveGetAllPngImages(string directory, List<string> imageList)
{
    imageList.AddRange(Directory.EnumerateFiles(directory, "*.png"));
    IEnumerable<string> subDirectories = Directory.EnumerateDirectories(directory);
    foreach (string subDirectory in subDirectories)
        RecursiveGetAllPngImages(subDirectory, imageList);
}
