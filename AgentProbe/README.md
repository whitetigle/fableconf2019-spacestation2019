# Agent probe!

This is a simple sample to show you how you can start probing the system uging fable. You can fork the repository and start from there if you wish.

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 2.1 or higher
* [node.js](https://nodejs.org) with [npm](https://www.npmjs.com/)
* An F# editor like Visual Studio, Visual Studio Code with [Ionide](http://ionide.io/) or [JetBrains Rider](https://www.jetbrains.com/rider/).

## Building and running the app

* Install JS dependencies: `yarn install`
* Install .NET dependencies: `.paket/paket install`
* Start compilation + watch mode: `yarn watch`
* Test : in a separate terminal : `yarn test` (you will need to fill `serviceToken` and  `gameKey` first in `App.fs`)

