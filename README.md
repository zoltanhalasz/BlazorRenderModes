# BlazorRenderModes

# Blazor Overview Video

[<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/1706203/266028119-0e52140c-db7c-40a9-920a-f10aa5b1f6bb.jpg" width="50%">](https://www.youtube.com/watch?v=u4azTLLGt8U "Blazor Render Modes")

# SSR Walkthrough Video
[<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/1706203/267058156-b510ab99-a1a8-4f8a-8714-859425987757.png" width="50%">](https://www.youtube.com/watch?v=2kGR1lgEL50 "Blazor Render Modes")

Blazor has 5 render modes. SSR Server Side Render, SSR Streaming Rendering, Blazor Server with SignalR, Blazor Wasm, Blazor Auto.
This repo has created demos for each render type.

The app has been built with .net 8 preview 7. To run the sample, you must download and install Visual Studio Preview and the .net 8 SDK.

Download Visual Studio Preview
https://visualstudio.microsoft.com/vs/preview/#download-preview

Download the SDK here
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

You will also need to get a TMDBApi developer key. Follow this link to get a key
https://developer.themoviedb.org/docs

To add your TMDBAPI key to the project you will navigate to the server project and edit program.cs 
You can add your key to the file or use user secrets like we did. We set the string variable with the value using a user secret. 

``` cpp
// your TMDB Read Access key must be in the server's secrets.json, e.g.:
// "TMDBKey": "your-API-key-here"
string tmdbKey = builder.Configuration["TMDBKey"];

builder.Services.AddScoped(sp => {
    var client = new HttpClient();
    client.BaseAddress = new("https://api.themoviedb.org/3/");
    client.DefaultRequestHeaders.Authorization = new("Bearer", tmdbKey);
    return client;
});
```


