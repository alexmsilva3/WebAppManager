using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration;
using System.Threading.Tasks;



namespace ConsoleAppManagerWIN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetConfiguration();

            downloadFile("https://github.com/alexmsilva3/3WEB_MVC/blob/master/3WEB.sln", @"D:\Teste.txt");

            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            Command command = new Command("D:\\command.ps1");
            //pipeline.Commands.Add("Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force");
            pipeline.Commands.Add(command);
            //pipeline.Commands.Add("Set-ExecutionPolicy - ExecutionPolicy Restricted -Force");
            Collection<PSObject> psObjects;
            psObjects = pipeline.Invoke();

            foreach (var item in psObjects)
            {
                Console.WriteLine(item);
            }
            
        }

        static void GetConfiguration()
        {
            var app = ConfigurationManager.AppSettings["APP"];
        }

        public static void downloadFile(string sourceURL, string destinationPath)
        {
            int bufferSize = 1024;
            bufferSize *= 1000;
            long existLen = 0;

            System.IO.FileStream saveFileStream;
            if (System.IO.File.Exists(destinationPath))
            {
                System.IO.FileInfo destinationFileInfo = new System.IO.FileInfo(destinationPath);
                existLen = destinationFileInfo.Length;
            }

            if (existLen > 0)
                saveFileStream = new System.IO.FileStream(destinationPath,System.IO.FileMode.Append,System.IO.FileAccess.Write,System.IO.FileShare.ReadWrite);
            else
                saveFileStream = new System.IO.FileStream(destinationPath,System.IO.FileMode.Create,System.IO.FileAccess.Write,System.IO.FileShare.ReadWrite);

            System.Net.HttpWebRequest httpReq;
            System.Net.HttpWebResponse httpRes;
            httpReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sourceURL);
            httpReq.AddRange((int)existLen);
            System.IO.Stream resStream;
            httpRes = (System.Net.HttpWebResponse)httpReq.GetResponse();
            resStream = httpRes.GetResponseStream();

            //long fileSize = httpRes.ContentLength;

            int byteSize;
            byte[] downBuffer = new byte[bufferSize];

            while ((byteSize = resStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                saveFileStream.Write(downBuffer, 0, byteSize);
            }
        }
    }
}

/*
1-Verificar config pra ver qual APP eu sou
2-Buscar lsita de comandos no Rabbit (implementação futura)
3-Buscar no git a tarefa a ser executada
3.1- Verificar "Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force"
4-Executar tarefa
5-Enviar resposta

*/