# The ludo game - avslutande projekt

I detta projekt ska ni implementera ett fia spel (Ludo på engelska), med knuff. Spelet **ska** vara en .net core konsol applikation.

Det ska gå att spela spelet via en konsol applikation, två till fyra spelare vid samma dator.

Koden ska vara uppdelat i en konsol applikation och en class library som innehåller all logik, låt oss kalla det vår spelmotor / game engine.

Spelet ska spara i en databas (med code first och Entity Framework), så att det går att ta fram historik på alla tidigare spel. Om applikationen skulle stängas ner, ska det gå att komma tillbaka in i spelet.

# Programmering

Kod ska ligga i mappen **Source**, varje team får enbart ha en solution fil!!

Se till att skåpa unit tests för spelet.

## Spelmotor (GameEngine)

Spelmotorn styra alla regler i spelet och kollar t.ex. om en spelpjäs får flyttas, om en spelar har vunnit, den initiala uppställning av alla spelpjäser på brädet, vilken spelar som är den nästa, hur en tärning ska bete sig, etc.

Implementera spelmotorn i ett separat klass bibliotek.

En instans av spelmotorn innehåller staten av ett helt spel, det skall vara möjligt att initialisera spelet med en given state, t.ek. om ska kunna spara och inläsa ett spel.

Skriv enhetstester på spelmotorn

# Dokumentation

Skriv [user stories](https://www.mountaingoatsoftware.com/agile/user-stories) (i docs mappen), och starta inte koda något innan in har skrivet minst 3 user stories, men helst så det täcker hela fia spelet, men se hela tiden till att lägga till fler user stories.

Om ni använder någon externa källor (båda kod och annat) ange dom i dokumentationen.

Dokumentation ska skrivas med markdown (.md), ni väljer själv om ni vill skriva på svenska eller engelska, markdown filerna placeras i **docs** mappen.

# Betygsättning

Tillsammans med projektet ska skåpas en [video](video_presentaion.md) som beskriver projektet.

**Deadline, video i Stream**: 2021-04-12, kl 23:59

**Deadline, kod på GitHub**: 2021-04-13, kl 23:59

## Koden
Ni kan göra så många branches baserat på *master* som ni önskar. När projektet är slut är det innehållet av main på ert **GitHub**-repo som räknas.

## Element i bedömning

* Process (**viktigt**), hur har ni kommat fram till det slutliga resultat
* Solutionen-filen ska beså av tre projekt:
  * En .NET 5 console-application
  * Class Library med en game engine / spel motor
  * Test projekt
* Koden kompilera och det går att köra projektet lokalt
* All logik som rör spelet, även kast av tärning, är placerat i spelmotorn
* Dokumentation av hur spelets element, klasser och funktionalitet (**viktigt**)
  * Ska finnas i Documentation-mappen
  * Dokumentationen måste uppdateras löpande
* Spelet ska spara i en databas (med code first och Entity Framework) (**viktigt**)
* Det ska gå att skåpa ett eller fler spel
* Det ska gå att försätta spela ett oavslutat eksisterende spel
* Automatiserade tester (**viktigt**) av spelmotor
  * Unit test
* Async kod (om det gir mening)
* Fluent API (om det gir mening)

Dom fyra element som är markerat med **viktigt** är så klart dom som är viktigast i samband med bedömningen. Och det är det som ni ska fokusera på i eran video presentation.

## Frivilliga element (kan göras som extra)

* Dokument databas
* Prestanda optimering
* Datatjänst
* AI spelare (så att man spela mot datorn) 
* Grafik representation av spelbräda (i konsollen)

# Tips

Tänka inte visuellt/grafisk när ni gör eran datamodell!

Där er <u>ingen krav</u> på verken Async eller fluent api, det viktigaste är att data sparas i en databas, att koden är testat med automatiserade test och att koden är lätt läst.

Gör en dagbok (journal / log) varje dag, också om ni gör något själv på en kväll, så att ni har koll på processen, och kan dokumentera den. Förslag gör det som markdown-dokument i *Dokumentation*-mappen.

# Video presentation

Do a video presentation of you project which explains your product, important features of your code and the process you have been through while developing your product.

The video should be some graphical presentation combined with a voice track. 

The video should have a max length of 30 minutes. Everybody in the group should have min 5 minutes of fame, in the sense that though out the video should each team member talk for at least a total of min 5 minutes, make it clear every time you change speaker.

The final movie should be a single file in the MP4 format and should be uploaded to Steam in the channel :  [Dataåtkomst Ludo game](https://web.microsoftstream.com/channel/ecb3fb85-c799-4287-9f1e-7d3fcee6310d) (which everybody hopefully have access to), include your group number in the description.

Release your creativity.

## Content of video

Focus in the video should be the following (it does not need to be in this order):

* A walkthrough of the thoughts behind the solution
  * Diagrams is a good thing
* A demo of the application running, it could be a video or screenshots
* Important parts of the code
  * Parts of the code you are proud of
  * Cool patterns / frameworks that you have used
  * Especially useful codereview comments
* A description of your process
  * Which tools did you use and for what?
  * How did the team communicate?
  * Did you part up your work in the team? And how?
* Lessons learnt during the process
  * What would you had done differently?

## Tools

There is no requirements to which tools you use, but a suggestions is to use the build-in recording functionality in **PowerPoint**, which makes it possible add a sound track (and video) to a presentation.  You could do screenshot of your code and put it into slides.

Screen recoding can be done directly in PowerPoint (see below). You can also use an external tool as the open-source tool [SimpleScreenRecorder](https://www.maartenbaert.be/simplescreenrecorder/), [OBS](https://obsproject.com/download) or any commercial tool (it's properly possible find a free trail). 

## Hint

Do a first version of the presentation video early in the process. To get a simple idea on what do present in the video and get familiar with the tool.

# HowTo

## Create a PowerPoint video

[How to Make a Video in PowerPoint - ppt to video](https://www.youtube.com/watch?v=D8JV3w4TOVw)

[PowerPoint Screen Recording Feature](https://www.youtube.com/watch?v=kQwGEY4IDi0)