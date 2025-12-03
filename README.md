# Advent of Code 2025

https://adventofcode.com/

Things have been so busy this year that I completely forgot about AoC until a coworker threw a message
into Slack earlier today.  I'm so ashamed.

```
These are your personal leaderboard times:

Day   -Part 1-   -Part 2-
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


