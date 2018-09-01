# OpenPagerXamarin

Android: [![Build status](https://build.appcenter.ms/v0.1/apps/7e8a9108-e629-4dec-bc05-20042d6cdc5c/branches/master/badge)](https://appcenter.ms)
iOS: [![Build status](https://build.appcenter.ms/v0.1/apps/48732a4e-0403-4595-8ff4-9fc64230962b/branches/master/badge)](https://appcenter.ms)

You will notice that there is no Info.plist or AndroidManifest.xml in the iOS and Android projects. Both of these files instead exist as tokenized templates in the build folder. The [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools) will use these to build automatically files with secrets.

## Getting the application to build locally

After cloning this repository to your local machine be sure to copy and rename the tokenized manifests from the build folder to the appropriate location.

**WARNING:** Android will automatically create a default AndroidManifest if you open Visual Studio or attempt to build before you have copied the template. Be sure to replace the tokens with the appropriate values from AppCenter for your Info.plist or remove this part.

Next create a file named `secrets.json` in the OpenPager project, and update it to look like the following:

```json
{
    "AppCenter_iOS_Secret": "",
    "AppCenter_Android_Secret": ""
}
```