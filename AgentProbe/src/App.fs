module App 

open Fable.Core
open Fable.Import
open Fable.Core.JsInterop
open Thoth.Json
open Fetch

Node.Api.``global``?fetch <- import "*" "node-fetch"

type Pong = {Pong:string}

let url = "https://27ecf629.eu-gb.apiconnect.appdomain.cloud/e5e446f5-cfc8-45ca-ba6b-322ef492c090/spacestation2019"
let serviceToken = "xxx"
let gameKey = "xxx"

let prepareHeaders body = 
    [ 
      requestHeaders [ 
        ContentType "application/json" 
        HttpRequestHeaders.Custom ("X-PLAYTHEWEB-ID" , serviceToken)
        HttpRequestHeaders.Custom ("x-game-key" , gameKey)
      ] 
      RequestProperties.Body <| unbox (body)
      RequestProperties.Method HttpMethod.POST
      RequestProperties.Mode RequestMode.Cors
    ]

module Commands = 
  // Do a ping request
  let ping() =
    promise {

      // prepare request json body using Thoth.Json
      let data = 
        Encode.object [
          "command", Encode.string "ping"
        ] |> Encode.toString 0

      let! result = fetch url (prepareHeaders data)
      let! json = result.text()
      return json
    }

let main() = 

  promise {
    let! json = Commands.ping()
    let decoded = Decode.Auto.fromString<Pong>(json) 
    match decoded with 
    | Ok pingMessage -> 
      JS.console.log (sprintf "Ping response message is %s" pingMessage.Pong)
    | Error err -> 
      JS.console.log (sprintf "Thoth.json Parsing Error: %s" err)

  } |> Promise.lift

