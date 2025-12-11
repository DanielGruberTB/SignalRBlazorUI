# SignalR Blazor UI

This project demonstrates real-time communication between a SignalR server and a Blazor Server application.

## Projects

- **Pitwallserver**: SignalR server that broadcasts messages every 5 seconds
- **SignalRClient**: Blazor Server application that receives and displays messages

## Running the Application

1. Start the SignalR server:
   ```bash
   cd Pitwallserver
   dotnet run
   ```

2. In a separate terminal, start the Blazor client:
   ```bash
   cd SignalRClient
   dotnet run
   ```

3. Navigate to the Blazor app in your browser (typically https://localhost:5001)

## How It Works

The SignalR server (`Pitwallserver`) broadcasts time messages every 5 seconds through the `TelemetryHub`.

The Blazor client (`SignalRClient`) connects to the hub and displays received messages in real-time on the Home page.

### Key Implementation Details

**Interactive Render Mode**: The Home.razor component uses `@rendermode InteractiveServer` to enable real-time UI updates. Without this directive, Blazor components run in static SSR (Server-Side Rendering) mode by default in .NET 8, which prevents dynamic UI updates even when SignalR messages are received.

**SignalR Connection**: The component establishes a SignalR connection in `OnInitializedAsync()` and subscribes to the `ReceiveNotification` event:

```csharp
hubConnection.On<string>("ReceiveNotification", (message) =>
{
    messages.Add(message);
    Console.WriteLine($"Message received from hub: {message}");
    InvokeAsync(StateHasChanged);
});
```

The `InvokeAsync(StateHasChanged)` call ensures the UI is updated on the correct thread when messages are received.

## Troubleshooting

If the UI doesn't update when receiving messages:
1. Ensure the component has `@rendermode InteractiveServer` directive
2. Check that `AddInteractiveServerComponents()` is called in Program.cs
3. Verify the SignalR server is running and accessible
4. Check browser console for connection errors
