### dependencias

- ASP .NET CORE MVC
- SQL SERVER

-------------

### IIS

- Instalar el IIS
- Activar todas las características de ASP en IIS
- Mover la carpeta northwind que esta en publish en la ruta `C:\inetpub\wwwroot` y dar los permisos correspondientes
- Abrir el adminstrador de IIS agregar nuevo sitio y fijar la ruta
```example
 C:\inetpub\wwwroot\northwind
 ```
 - Instalar el  ASP.NET Core Hosting Bundle
 ```example
 https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.4-windows-hosting-bundle-installer
 ```
-------------

### SQL SERVER

- Importar el instnwnd.sql que se cuentra el solution
- Abra SSMS en el panel izquierdo security/Logins click derecho new login
- En el cuadro de diálogo Login - New ponga `IIS APPPOOL\northwind`
- En la sección de la izquierda, selecciona User Mapping marque la base de datos `northwind`
- En la sección inferior, selecciona los roles de base de datos, como: `db_datareader` lectura y `db_datawriter` escritura y guardar
- **Ajustar la cadena de conexión**:
   - Abrir el archivo `appsettings.json` en la carpeta `C:\inetpub\wwwroot\northwind` y modificar la cadena de conexión para que apunte a su servidor SQL y base de datos.
- Reiniciar el IIS
-------------