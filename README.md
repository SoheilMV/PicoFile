# PicoFile
* Convert PicoFile upload center links to direct link
* It has 20 GB of free space

## using
````csharp
using PicoFile_Direct_Link;

PicoFile pic = new PicoFile();
try
{
    pic.URL = http://s8.picofile.com/file/8355584542/direct_link.png; //Enter the file address
   
    string link = await pic.DirectLink(); //Receives direct link
    //Or
    string link = = await pic.DirectLink("File password"); //Receives direct link
    
    Console.WriteLine(link); //Show direct link
}
catch (PicoFileException pfx)
{
    Console.WriteLine(pfx.Message)
}
````
