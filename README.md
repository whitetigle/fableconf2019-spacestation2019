# Space Station 2019. 
A FableConf workshop by [François Nicaise/@thewhitetigle](https://twitter.com/thewhitetigle)

## Story

**Official statement.  May 2019.**

“We’ve been very happy with the latest development of our 100% AI driven Space Station project. The AI should be able to evolve without any human interaction and happily help solve all the problems our human team should encounter.” 

**News coverage. June 2019**

“According to several anonymous sources, it seems that Space Station 2019’s AI locked itself into some mute state. The project team can’t get access to the Space Station anymore”

**Official statement. July 2019**

“It’s with sad regrets that we have to announce that we lose control of Space Station 2019. But our team is working hard to get control back.”

**Official statement. August 2019**

“We need help. Our main engineer is locked inside. We need to release him as soon as possible.”

**Official statement. September 2019**

“We’re now opening our systems to hackers worldwide. Space Station 2019’s AI has modified its API. We need to discover its new API and release our engineer.”

## Game rules
The goal of the game is to locate and rescue the engineer locked in Space Station 2019. We don’t know his/her name. We need to find a way to gather data about his/her whereabouts and unlock all the doors to make the release possible. 

## Missions

### Get Access To System
  - Try to find how to login
  - Try to find credentials 
  - Login (find the command) using `{"command":"???";"user":"???";"pass":xxx}`
  - Get access key, retrieve list of available commands and start the probing (don’t forget to include ``accessKey`` in your messages)

### Prepare release
  - Make sure Air System is up and *all* rooms are unlocked
  - Find Engineer’s location
  - Talk to engineer to check if is out!
  - Finally confirm escape status thanks to very convenient command provided by Space Station Team: ``checkEscapeStatus`` :wink:

## Rules of the Workshop 

The aim of this workshop is to make people use Fable to develop client and server solutions based on F# and Fable on Node.js environments.

Participants will be splitted into teams of two or more peoples (one player is also possible but should be less funny)

### Roles



#### Agent Probe
**Mission**: probe and find the Space Station hidden API paths to make the release of locked engineer possible. 

**Requirements**: use Fable (curl or postman are way too easy and obviously you want to learn a thing or 2)

**Tip:**: 
  - check [fable samples](https://github.com/fable-compiler/fable2-samples).
  - check `Agentprobe` subfolder for a ready to go Elmish+React client
  

#### Agent Display
**Mission**: Collect data using Agent Probe’s findings and display data on a web page.
**Requirements**: use Elmish + React 

**Additional requirements are the following:**
- F# and Fable installed

**Tip:**: 
  - check [fable samples](https://github.com/fable-compiler/fable2-samples). 
  - check `AgentDisplay` subfolder for a ready to go Elmish+React client

**important:** 
  - The address and creadentials for  game server will be given when workshop starts.

## Starting the game

Since, you will have to add some headers too for you http requests. We will communicate them to you when your team is ready as well as the server url.
Then Agent Display can start creating a frontend while Agent Probe starts probing the remote server to log into the system
Starting command is: ``{"command":"ping"}``

### Protocol

- use only ``POST`` requests
- Messages will be sent and received using **json**
- Ingame commands are part of every JSON you will send. Example: ``{"command":"nameOfCommand",...}``

*Have fun and good luck!* 
