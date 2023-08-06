# Rasta Stream

## Azure Open AI / .NET / HTML/JS with Streaming Chat Responses

Chatty LLMs like GPT-3.5 generate their output token after token.
That's great, but can sometimes take a while until the LLM is done
and has the complete response for us. In many applications it can
be nice to present to the end user the response from the bot as it
is being produced. That's easy to do with the chat completion API
and the corresponding streaming methods in the .NET Open AI client.
When passing the reply through a server and to a frontend client it
is important to stream all the way through, and at both the server
and the client, read the response as it is being produced and
display each new chunk immediately as it arrives. This example
demonstrates doing this with a very simple .NET HTTP server and an
even simpler HTML/JS frontend.

To run the example:
1. Copy `.env.example` to `.env` and edit it to provide the values for your Azure Open AI deployment
2. `> dotnet run`
3. Surf to `http://localhost:8080/`
4. Say something and hit Submit. The responses are always in Rastafari rhyming style.
5. See how the reply from the bot is being presented progressively?

```
You: tell me about streaming chat responses

Bot: In this digital space where ideas flow,
Streaming chats bring a vibrant glow.
With fingertips dancing across the keys,
We connect and share with such ease.

Chat responses appear in a rapid stream,
Bringing together minds like a dream.
From far and wide, we come together,
In this virtual realm, we find our tether.

Through the waves of cyberspace we roam,
Expressing thoughts from the comfort of home.
In this tapestry of words, we intertwine,
Creating connections that feel so divine.

Yet, let us remember, in this virtual place,
Kindness and respect should always embrace.
For behind each screen, a person resides,
With emotions and feelings that cannot be denied.

So let us chat with love and compassion,
Spreading positivity in every interaction.
In this streaming chat, may understanding grow,
As together we learn, evolve, and glow.
```