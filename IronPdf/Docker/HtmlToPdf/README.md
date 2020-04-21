# Html to Pdf - Docker tutorial

Structure:
  * [_HtmlToPdf.Console_][1] - .NET Core Console application that converts HTML to PDF.
  * [_Dockerfile.windows_][2] - the Dockerfile for running [_HtmlToPdf.Console_][1] under Windows containers
  * [_Dockerfile.linux_][3] - the Dockerfile for running [_HtmlToPdf.Console_][1] under Linux containers

## Initialization steps
  * [Pull][5] the docker files and the sample application:
```
git clone https://github.com/iron-software/tutorials.git
```

  * Change the current folder to this tutorial context by the next command
```
cd .\tutorials\IronPdf\Docker\HtmlToPdf
```

## Steps for the Docker with Windows container:
  * Switch the Docker to Windows containers:
Use the menu item 'Switch to Windows containers...' from the [Docker Desktop][4] or the next command
```
C:\Program Files\Docker\Docker>DockerCli.exe -SwitchWindowsEngine
```

  * Build the Docker image based on Windows Core Server:
```
docker build --rm -t htmltopdf.windows -f Dockerfile.windows .
```

  * Run the Docker image
Replace the place holder `%PDF-RESULT%` by a path to result folder, e.g. `C:\temp\pdf-result` (the path should be exist before run the command).
```
docker run -v %PDF-RESULT%:c:\pdf-result --rm --user=ContainerAdministrator htmltopdf.windows https://google.com c:\pdf-result\google.com.pdf
```

The result should be like that:
```
    C:\>docker run --rm --user=ContainerAdministrator -v c:\temp\pdf-result:c:\pdf-result htmltopdf https://google.com c:\pdf-result\google.com.pdf
     IronPdf 2020.3.2.0 sample: converts HTML to PDF
     Arguments:
             html=https://google.com
             pdf=c:\pdf-result\google.com.pdf
     
     IronPDF:Chromium initialized (msgid 80010106)
     Result saved to: c:\pdf-result\google.com.pdf
```

## Steps for the Docker with Linux container:
  * Switch the Docker to Linux containers:
Use the menu item 'Switch to Linux containers...' from the [Docker Desktop][4] or the next command
```
C:\Program Files\Docker\Docker>DockerCli.exe -SwitchLinuxEngine
```

  * Build the Docker image based on Windows Nano Server:
```
docker build --rm -t htmltopdf.linux -f Dockerfile.linux .
```

  * Run the Docker image
Replace the place holder `%PDF-RESULT%` by a path to result folder, e.g. `C:\temp\pdf-result` (the path should be exist before run the command).
```
docker run -v %PDF-RESULT%:/pdf-result --rm --user=ContainerAdministrator htmltopdf.linux https://google.com /pdf-result/google.com.pdf
```

The result should be like that:
```
    C:\>docker run --rm --user=ContainerAdministrator -v c:\temp\pdf-result:/pdf-result htmltopdf https://google.com /pdf-result/google.com.pdf
     IronPdf 2020.3.2.0 sample: converts HTML to PDF
     Arguments:
             html=https://google.com
             pdf=/pdf-result/google.com.pdf
     
     IronPDF:Chromium initialized (msgid 80010106)
     IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig == true
     Attempting to automatically install Linux / Docker dependencies.  This may take some time on your first run on this machine.
     ...
     Linux / Docker dependency installation success
     Result saved to: /pdf-result/google.com.pdf
```

[1]: ./HtmlToPdf.Console
[2]: ./Dockerfile.windows
[3]: ./Dockerfile.linux
[4]: https://www.docker.com/products/docker-desktop
[5]: https://git-scm.com/docs/git-pull
