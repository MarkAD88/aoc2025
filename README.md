# Advent of Code 2025

https://adventofcode.com/

Things have been so busy this year that I completely forgot about AoC until a coworker threw a message
into Slack earlier today.  I'm so ashamed.

```text
These are your personal leaderboard times:

Day   -Part 1-   -Part 2-
  8   12:07:35          -
  7   00:47:45   02:01:44
  6   00:24:03   01:29:56
  5   00:19:01   00:39:55
  4   00:24:43   00:36:53
  3   00:17:30   03:00:21
  2   17:49:03   18:17:59
  1       >24h       >24h
```

## Day 1 - Secret Entrance
Part 1 was cake walk.  Part 2 ruined me.  I wasn't handling edge cases properly and it took me forever to
track it down and resolve it.  Off by one errors will forever be the bane of humanity.

## Day 2 - Gift Shop
This one was really easy.  Nice break from the pain of Day 1's part 2.  I had to refactor part 2 to use
`long` instead of `int` but other than that it was straight forward.

## Day 3 - Lobby
Part 1 was super easy.  Part 2 murdered me.  I tried two simple approaches one after another that got
ridiulously complicated when I tried to handle all the edge cases that I encountered.  The third solution
still has a too-short edge case handler and a remainder-too-short edge case handler but at least it
finally works.  Except for a few very rare instances, part 2 always kicks my teeth in.  Ugh.

## Day 4 - Printing Department
Finally I feel like a real programmer.  Both parts were super stright forward and I knocked them out
quickly.  Would have been quicker but I forget a variable assignment on the first part and probably
cost myself 10 minutes staring at incorrect results when the problem was staring me right in the face.

## Day 5 - Cafeteria
Part 1 was easy but I tripped myself up by trying to use a tricky LINQ TakeWhile to parse the first
half of the file (fresh ingredients) and another LINQ TakeWhile to parse the second half of the file
(available ingredients).

Turned out that File.ReadLines doesn't do a "continuation" of the iterator in another loop it just
starts over so that didn't work out too well and cost me.

Then the approach I originally took - creating a List of longs for the fresh ingredients to use
in an intersect with the fresh ingredients - worked fine for the example but once it hit the actual
input it blew an OOM fast because the ranges were just too large to deal with.

I switched to just tracking the start/end of a range using a custom class and that did the trick.

Part 2 wasn't necessarily hard but I overcomplicatd my solution at first by trying to sort and then
expand ranges by altering items and what not.  Horrible idea in hindsight.  I scrapped that and went
with a simpler counter with min/max checks to get it done efficiently.

Start to finish under an hour so just a few minutes off from Day 4's time.  Fun puzzle.

## Day 6 - Trash Compactor
Part 1 wasn't necesarily hard but I struggled a bit with the parsing logic which cost me a bit of time
but finally figured out the right approach.

Part 2 nearly beat me.  Once again I over complicated the parsing solution which lead me way down the
wrong path.  Once I saw the light and simplifed the parsing the rest was a breeze.

## Day 7 - Laboratories
Do I even need to say it again?

Part 1 proved somewhat challenging.  I kept getting incorrect results for the examples regardless of
what technique I used.  Turns out I my logic was 100% correct but I was trying to use a hashset to
keep track of my Beams and was mutating the state (x and y positions) which caused my uniqueness
checks to fail resulting in wildly incorrect results.  Once I figured out what I was doing wrong
a minor fix made everything flal into place.

Part 2.  Ugh.  I must have spent 45 minutes trying to figure out if there was a simple mathmatical
formula I could use to calculate the result.  If there is one SOMEONE please tell me what it is
because I couldn't figure it out.  So I took the brute force approach.  Well, maybe not 100% brute
force.  The caching approach is a learned technique from pervious year AoC problems.  My numbers
still weren't coming out properly but at least they were wrong FAST instead of SLOW.  The root
of the problem turned out to be me caching information with the wrong coordinates.  I wasn't
frezzing the input Y coordinate that was passed in to the method and was instead using the
incremented Y coordinate when caching results leading to a whole lot of bad.  Alls well that
ends well though.

Still, at least it didn't take me as long to solve as Day 3.  :smile:

## Day 8 - Playground
Part 1 should have been painless.  Simply calc the distances between all the nodes.  Group them
together by said distances until you've made {x} connections and you're done.  But no.  I
just couldn't be bothered to check that my MATH was wrong and I wasn't ensuring that my
distance calculation was accurate.  That little mistake cost me 2+ hours last night - I finally
gave up after 2 hours and 45 minutes - and took me all of 5 minutds to solve this morning
when I just glanced at the code as I was closing out the editor.  Ugh.

Never figured out how to implement Part 2.  I'll have to come back to that another day.

## Day 9 - Movie Theater
Part 1 was simple and painless.  Finished it in a matter of minutes.

Part 2 I know what I want to do - order the red tiles as vertices in a polygon so I can use a
standard containing algorithm but I cannot get the order of the tiles right.  ARGH!

Part 2 Update:
I came up with a brute force approach to Part 2 and it worked perfectly on the sample data but
when I tried it on the full input it just blew up with an OOM.  BUT the upside is it laid the
groundwork for determining per Y valid ranges for X which is what I finally used to solve the
thing.  It's relatively fast too.
