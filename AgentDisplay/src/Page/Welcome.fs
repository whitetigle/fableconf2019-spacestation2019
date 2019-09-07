namespace Page

open Types
open Fable.Core
open Elmish
open Fetch


module Welcome = 

  module Types = 

    type Model = 
      {
        PingMessage:string option
        Loading:bool
      }

    let initModel = { PingMessage=None; Loading=false}

    type Msg = 
      | Ping
      | OnPing of Result<Pong,string>
      | OnDisplayError of System.Exception


  module Commands =

    open Types

    let getResult handler response = handler response
    let getError handler response = handler response

    let fetchPing model=          

        let command = 
          Cmd.OfPromise.either
            Fetch.Api.ping
            ()
            (getResult OnPing)
            (getError  OnDisplayError)

        model, command

  module State = 

    open Types
    open Elmish
    
    let update (msg:Msg) (model:Model) = 
      match msg with 
      | Ping -> 
          { model with Loading=true} |> Commands.fetchPing

      | OnPing ping-> 
        match ping with 
        | Ok ping -> 
          {model with PingMessage=Some ping.Pong; Loading=false}, Cmd.none

        | Error fetchError -> 
          {model with PingMessage=Some "error"; Loading=false}, Cmd.none // we can do better than this

      | OnDisplayError _-> 
        {model with PingMessage=Some "Something bad happened"; Loading=false}, Cmd.none // we can do better than this

  module View = 

    open Types
    open Fulma
    open Fable.Core.JsInterop
    open Fable.React
    open Fable.React.Props

    let root (model:Model) dispatch = 

      let pingMessage = 
        match model.PingMessage with 
        | None -> "??"
        | Some m ->  m

      div [] [
        Columns.columns [] [
          Column.column [ Column.Width(Screen.All, Column.Is3)] [ 
            Notification.notification[ Notification.Color IsBlack] [ Heading.h4 [] [str "Ping command result"]]]
          Column.column [ Column.Width(Screen.All, Column.Is2)] [ 
            Notification.notification[ Notification.Color IsInfo] [ str pingMessage]
          ]
          Column.column[] [
            Button.button [
              Button.Props [
                OnClick ( fun _ -> Ping |> dispatch)
              ]
            ] [ str "refresh"]
          ]
        ] 
      ]