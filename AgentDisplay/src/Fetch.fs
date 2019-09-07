module Fetch.Api

open Thoth.Json
open Fable.Core
open Fable.Core.JsInterop
open Types

[<Literal>]
let serverURL = "https://27ecf629.eu-gb.apiconnect.appdomain.cloud/e5e446f5-cfc8-45ca-ba6b-322ef492c090/spacestation2019"

[<Literal>]
let serviceToken = "xxx"

[<Literal>]
let gameKey = "xxx"

module Query =
      
  let fetch (url: string) (init: RequestProperties list) = //: Promise<Result<Response,FetchError>> =
      promise {
        let! response = GlobalFetch.fetch(RequestInfo.Url url, requestProps init)
        return response
      }

  let Post url (body:string) =
    let withProps body = 
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

    promise {
      let! result = 
        body
          |> withProps
          |> fetch url

      let! text = result.text()
      return text
    }


let ping () =
  promise {
    let body = 
      Encode.object [
        "command", Encode.string "ping"
      ] |> Encode.toString 0

    let! json = Query.Post serverURL body
    let decoded = Decode.Auto.fromString<Pong>(json) 
    return decoded
  }