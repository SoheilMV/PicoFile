# PicoFile

![PicoFile](Images/PicoFile.png)

* Convert PicoFile upload center links to direct link
* It has 20 GB of free space

## using
````csharp
using PicoFileSharp;

try
{
    PicoFile picoFile = new PicoFile("https://s29.picofile.com/file/8462040100/WinRAR_6_02.zip.html"); //Enter the file address
    string link = await picoFile.DirectLink(); //Receives direct link
    //Or
    string link = await picoFile.DirectLink("File password"); //Receives direct link
    
    Console.WriteLine(link); //Show direct link
}
catch (PicoFileException pfx)
{
    Console.WriteLine(pfx.Message)
}
````
