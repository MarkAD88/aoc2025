# Advent of Code 2025

https://adventofcode.com/

Things have been so busy this year that I completely forgot about AoC until a coworker threw a message
into Slack earlier today.  I'm so ashamed.

```text
These are your personal leaderboard times:

Day   -Part 1-   -Part 2-
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
