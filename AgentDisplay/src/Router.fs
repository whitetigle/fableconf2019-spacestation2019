module Router

open Fable.Import
open Fable.React.Props
open Elmish.Navigation
open Elmish.UrlParser
open Types

let private toHash page =
  
  match page with
  | Route.Welcome -> "#welcome"

let pageParser: Parser<Route->Route,_> =

  oneOf [
    map Route.Welcome (s "welcome")
  ]

let href route =
  Href (toHash route)

let modifyUrl route =
  route |> toHash |> Navigation.modifyUrl

let newUrl route =
  route |> toHash |> Navigation.newUrl

let modifyLocation route =
  Browser.Dom.window.location.href <- toHash route
