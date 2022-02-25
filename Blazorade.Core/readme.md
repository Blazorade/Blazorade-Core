# Blazorade Core

Provides core functionality for your Blazor component libraries as well as many of the Blazorade libraries, such as [Blazorade Bootstrap](https://github.com/Blazorade/Blazorade-Bootstrap) and [Blazorade MSAL](https://github.com/Blazorade/Blazorade-MSAL).


## Getting Started

After you've installed this package to your application, please refer to the [Getting Started](https://github.com/Blazorade/Blazorade-Core/wiki#getting-started) section on the project Wiki to quickly get started with Blazorade Core.

## Highlights

- [`ComponentReconnectModal`](https://github.com/Blazorade/Blazorade-Core/wiki/ComponentReconnectModal) - Allows you to customize the UI that is shown when your Blazor Server application loses connection with the server.
- [`DotNetInstanceCallbackHandler`](https://github.com/Blazorade/Blazorade-Core/wiki/DotNetInstanceCallbackHandler) - A utility class that allows you to easily call into JavaScript function that provide a result using  callback. Your Blazor application uses the standard .NET `async`/`await` mechanism.
- [`Redirect`](https://github.com/Blazorade/Blazorade-Core/wiki/Redirect) - A utilicy component that redirects the user to another URL, but only if the component is rendered. Allows you to easily do conditional redirecting, for instance to send anonymous users to a login page for instance.