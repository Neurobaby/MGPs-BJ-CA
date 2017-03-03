# MGPs-BJ-CA
MGPs Blackjack Combinatorial Analysis.  Exact calculations including splits and insurance.

MGP's BJ CA Help
Contents
1 Introduction
Page 2
2 File System
Page 3
3 Configuration Options
Page 4
4 Forced Strategy
Page 5
5 Bonus Rules
Page 6
6 Real Time Analysis
Page 7
7 Calculation Options
Page 8
8 Results Analysis
Page 10
9 Features List
Page 12
10 Acknowledgements
Page 13
2
Chapter 1
Introduction
Welcome to MGP’s BJ Combinatorial Analysis program. This program attempts to calculate as close as
possible without brute-force techniques various expected values and strategies for the game of Blackjack. If
you ever have any question please feel free to email me directly.
Since this is not a commercial program and because it would take too long, I will assume that the user has
some background in both blackjack and the combinatorial analysis of blackjack. If you want to skip all this
then just press Calculate Now!
I tried to make the program as self-explanatory and comprehensive as possible, but that’s from the viewpoint
of the programmer, which is of course going to be warped somewhat. So in an effort to allow anyone who
has any interest in playing with the program, I’ve decided to follow everyone’s advice and write this file.
The next two chapters simply explain what the various options are in the menu including the file system.
These are probably the least self-explanatory options. The chapters after that explain what I consider to be
the two most powerful features and that’s the how to use the Forced Strategy and Bonus Rules. Next will be
an explanation and some advice with regards to the Real Time Analysis forms. The following chapter will
be an item by item description of all the options offered will follow including a description of whether the
calculations are exact, thought to be exact or estimates. Finally, a description of all the analysis options will
follow including how to generate EORs and N-Card strategies.
This program is the result of too many hours of time spent programming by an amateur. As any programmer
or user of a Microsoft program knows, as hard as I’ve tried there probably are some residual bugs, I’m just
not sure what they are 
Before we get any further though there is one thing that needs to be explained and that is the difference
between a Total Dependent (TD), 2-Card Dependent (2C or TC), and Composition Dependent (CD) strategy.
TD means exactly that. All hands are played solely based on their total and the upcard and the strategy is 0-
card dependent. If a pair is not split, or is reached after split, it is played solely based on its total.
2C strategies play the first 2 card hands based on their composition, and all 3 or more cards based solely on
their total. The same strategy is used after splits – i.e. all two card post-split hands use their pre-split strategy
and all 3 or more card hands are played based on their totals.
CD strategy is composition dependent and each hand is played optimally based on all the cards played in the
hand.
All calculations are thought to be exact for top-of-the-deck for a single player with the exception of the
various BBO rules.
3
Chapter 2
File System
Rule Set File
The file system is actually probably more complicated than it needs to be, however I wanted to make it as
flexible as possible. The main kind of file that you will become familiar with is the Rule Set File. These
files will fill every option of the entire form. It will overwrite any forced or bonus rules including the tables,
the color options, and everything else. This is the easiest type of file to use and so it is the main file. The
one draw back of using these files was already mentioned, because they overwrite the bonus rules and forced
rules, any rules not in the Rule Set File will be erased. The INI file is an example of a rule set file.
Double Table, Forced Shoe and Color Table Files
These files simply save the settings for the tables described. If for instance you wish to have different color
schemes, just simply save the various color tables and load them at will. The same is true for the forced shoe
and the double table. Make sure that if you wish the forced shoe to be used that option is selected and same
for the double table.
Forced Table File
This type of file simply fills in or saves the tables for the forced rules as well as how the N-card setting for
the forced strategy and whether the tables apply pre- or post-split.
Forced Rules List and Bonus Rules List Files
Both of these files are used to fill their respective lists. Loading a file will overwrite the current list.
4
Chapter 3
Configuration Options
New File
This will erase all of the current settings and restore the current defaults.
Set Defaults
This will save all of the forms current settings in the INI file. The next time the form is loaded it will have
all of the same options including the forced shoe, color scheme, forced and bonus rules tables and lists.
Restore Original Defaults
This will restore the original default settings overwriting all current settings.
Use and Update Dealer Probs File
In an effort to save calculation times, as the CA is used, the program will save the dealer probabilities in a
data file. As you can imagine, the files can get awfully large. So much so in fact that as the files get to be
over a few megabytes they actually slow the program down because of the time it takes to load and save the
data files. In an effort to decrease the loading and saving times, the program will save a different file based
on the number of decks, whether they are Spanish decks or regular, and whether or not the dealer stands on
17. All forced shoes will be saved in a single file.
When the use dealer probs option is on, any previously calculated probabilities will simply be loaded. If the
option for updating the files is checked, any new probabilities will be added to the respective files. If a file
gets too large then just delete it from the directory to reset the file.
Include Post-Split Excpetions
These can be numerous and time consuming to compute and are generally not useful so the option is there,
and the default is set to turn not enumerate them.
Upcards Allowed
If there is a single upcard or set of upcards of interest, then using this table can be a nice time saver.
Compute TD, 2C, Forced
These options allow you to turn off the computations of various strategies as a time saver.
Color Table
This table let’s you define your own colors that will be used throughout the forms.
5
Chapter 4
Forced Strategy
This is both one of the most powerful and most confusing features of this CA both to use and definitely to
write. The forced strategy is what allows the calculation of N-Card strategies and EORs as well as the
comparison between different plays.
One very important thing to realize about the forced strategy though is that the strategies for pairs can only
be assigned in the Pairs Forced Strategy section and with an Exact Match Forced Rule. So if for instance
you wish to stand all 16 v 10 post-split, and the usual strategy for 88 is PH, then you should create a forced
rule just for 88 v 10 to make it stand in addition to the more general rule.
The first thing to decide when using the forced strategy is how n-card dependent you’d like it to be. If you
want a TD strategy set it to 0. If you want a 2-card dependent strategy, set it to 2. For a CD strategy set it to
21. All hands that have <= the n-card CD setting will be played optimally and any hands with more cards
will be played based on a total-dependent strategy. The total dependent strategy will be determined without
the inclusion of hands that are played optimally or forced.
Any hand that does not have an associated forced rule from either the table or the list will be played with the
optimal strategy based on how many cards are in the hand. If the hand has <= the setting for n-Card CD,
then the hand will be played composition dependent. If the hand has more cards than the n-Card CD setting
for the forced strategy, it will be played based on the optimized TD strategy which will appear in the tables.
Hands that are forced are not included in the determination of the table’s strategy.
The tables are used to enter in the TD part of the forced strategy as well allowing for the starting 2-card
hands. You can change an entire row at once by clicking on the row heading.
The most flexible part of the strategy is the Forced Rules List. This allows you to make up any forced rule
that you like that will be applied pre- or post-split based on either the number of cards, total or even
composition. It is this list that is used to create an N-Card Strategy as described in the results section in
Chapter 8.
Also described in the results section will be how to use the forced strategy to calculate EORs. The strategy
can also be recalculated without needed to redo all the calculations.
NOTE: No warning dialog pops up if you forget to update or add a rule when changing it. The rules will
also not be automatically turned on if the box is not checked.
6
Chapter 5
Bonus Rules
This another one of the most powerful and most confusing features of this CA. While the CA has many
options for various rules and some bonuses, this functionality was added to allow for great flexibility.
The preset bonuses include allowing BJ bonuses to be paid after splits, suited BJ bonuses, and player 21
always wins, even against BJ and after doubling. The latter rule is found in the Special rules section.
All other bonuses however are defined in the bonus rules section. After defining the hands to which the
bonus will apply and deciding if it pays pre- and/or post-split, the remaining options need to be filled in. If a
bonus does not apply, just set the payoff to 0. If it loses to BJ make sure the BJ payoff is -1. For instance, if
you have a Suited 678 pays 3:1 bonus, set the general payoff to 0 and the suited payoff to 3.
The bonus by default will automatically win. If the Hand Continues option is checked, then the bonus is paid
immediately and the hand will continue for its usual EV. If the hand must win then the payoff will only be
paid if the hand wins.
A bonus can also be paid if the hand must be against a particular dealer upcard by setting the upcard.
The bonus rules will not be paid post-double.
The implementation of the bonus rules is one of the aspects of the CA that has not been confirmed by
simulation or others’ calculations except for non-suited hand wins bonuses. I have however, to the best of
my ability, verified that the calculations are acting as I intended them to.
NOTE: No warning dialog pops up if you forget to update or add a rule when changing it. The rules will
also not be automatically turned on if the box is not checked.
7
Chapter 6
Real Time Analysis
This is a feature that’s use was basically killed with the passage of legislation banning money transfers to
and from gambling sites in the US. The feature was designed to allow both accurate and rapid calculations
for an online player.
There are 2 choices the user can make with regards to the feature. The first is whether or not to use single
split estimates. If the intention is to use the feature for online play than I highly recommend using this
setting as it increases the calculation speed significantly and the strategy is rarely dependent on how many
splits are allowed. The way this option works is upon loading, it will calculate the net EV of the original
shoe and then redo the calculation with only 1 split allowed and the difference is calculated. For subsequent
new round EV’s, the calculation is done with a single split allowed and the difference in EV from reducing
the number of splits is added.
The Options
The next option is whether to use the large or small form. The large form is more useful if not playing online
but just exploring as it is pretty big and can obstruct a significant portion of the playing area. The large form
however allows manual adjustment and visualization of the shoe as well as provides room for multiple
different players’ hands.
The real time analysis does allow the application of bonuses but only pre-split. Post-split bonuses were a
feature I added later and I have no plans on adding them now.
How to Use
When using the forms, you can enter each player’s cards in the appropriate boxes. If you make a mistake
you can press the Restore Previous Shoe button to restart the round. This option is not available in the small
form. After a round is completed press the New Hand button. The current shoe will be used for the new
calculations. Finally, when there is a reshuffle, press the Original Shoe button. Note that the real-time forms
do not directly support post-double strategies. They can be indirectly supported by allowing double on any
number if redoubling is allowed and surrender any number if double-down rescue is allowed. It would then
be necessary to use the Large Form and to manually pick the best allowed EV from the list of EVs.
Best Use
In reality however, when playing online there is rarely time to fill in every other players’ cards and the
dealer’s cards in multiple boxes in order. In fact usually there is barely enough time to play out your hand
optimally. The best way in my opinion to take advantage of the real time analysis is to do the following
unless you have at least 30s per hand:
1) Use the small form
2) Use the Overall EV to adjust your bet size and make insurance bets
3) Use a memorized strategy to actually play the hands out
4) Enter all cards in one box and then recalculate at the end of the round
8
Chapter 7
Calculation Options
Hole Card Rules
OBO: Only original bets are taken if the dealer has BJ. All values against A and Ten are conditioned on the
dealer not having BJ.
ENHC: All bets on the table are collected if the dealer has BJ. If the dealer check is set to true against A or
T then EVs are conditioned on the dealer not having BJ as in OBO. This includes all doubled and split bets.
BBO: Busted bets plus one. In this case all busted bets are collected immediately and then if any remain on
the table one more is removed.
Australian OBBO: This is how Original and Busted bets is played in Australia. In this case 1 bet from each
hand is collected after a split if the hand has not busted regardless of whether or not the hand was doubled
post-split.
Literal OBBO: Original Bets and Busted Bets Only. This used the literal interpretation of the rule although
finding the game played this way is difficult. In this case the very first hand in a set of splits is considered to
be the original bet so if it busts, no other bets are collected. If it is not busted and doubled or not, only one
bet is collected.
The above 3 BBO rule calculations are all estimates. For situations in which bonuses are not paid against BJ
they are accurate to 4-5 decimal places. For calculations such as those that would include double-down
rescue or bonuses against dealer BJ that pay early, the calculations are accurate to 2-3 decimal places.
Double Rules
Doubled aces count as one: If a hand with an ace is doubled, it only counts as 1 and not 11. If a second ace
is received then the that ace can count as 11. This rule is sometimes restricted to hands totaling 19 pre-split.
Player 21 always wins including post-double: This rule will also force a multi-card player win against BJ for
ENHC and the Busted Bets options.
Double Down Rescue: This is the option to turn on if you are allowed to surrender after doubling. When the
dealer is not checking for BJ, and the bets are resolved immediately, use the DDR Early option. If the dealer
checks for BJ before the surrender is resolved use the DDR Late option. DDR is always played optimally,
even during EOR calculations.
Redoubling: The player is allowed to redouble a set number of times depending on what is entered in the
Number of Doubles Allowed box. Redoubles are always played optimally, even during EOR calculations.
9
Surrender Rules
Surrender bonus against dealer BJ: This option is used if there is a different payout against dealer BJ. It can
obviously only be used with early surrender.
5-Card Surrender: This option is a result of my misunderstanding of Macau surrender. This will allow
surrender for player hands consisting of exactly 5 cards. In fact the Macau rule is a surrender half-win rule
and must be entered in the Bonus Rules section.
Split Rules
Split Table: Defines which pairs are allowed to split and which aren’t. In England for instance some pairs
are not allowed to be split.
CDZ-: This post-split strategy uses the same CD strategy that was used before splitting for each hand.
CD-P: This strategy will calculate the best post-split CD strategy for each hand only taking into account the
hand composition, dealer upcard and how many pair cards were removed.
CD-PN: This strategy is similar to a CD-P strategy but also accounts for the number of generic non-pair
cards are removed. If you have no idea what this means then don’t worry about it 
Special Rules
Player 21 always wins including post-double: This rule will also force a multi-card player win against BJ for
ENHC and the Busted Bets options.
Player-Dealer Ties: This table allows you to define how player-dealer ties are paid based on the total.
10
Chapter 8
Results Analysis
This section explains the options for the results analysis. Again, only things that are not self-evident will be
explained.
Suited Hands
Suited hands are always played optimally, even for TD Strategies. This was necessary do to the complexity
of the coding. This is also relevant as they are played optimally even during EOR calculations so when
suited bonuses are possible, EORs will not add to 0 as they should.
N Card Analysis
These values are obtained based on the calculated EVs. When a strategy would change the hands that are
played, the EVs are not recalculated.
Double Analysis
Only hands that are relevant to doubling are listed. The post-double strategy is listed as well as the details of
what happens when the hand itself is doubled. If you take the sum of all the weighted EVs, you would need
to multiply it by to match the Double EV.
Exceptions
This section not only lists all the exceptions to a strategy based on the strategy’s EVs, but also lets you send
the exception to the Forced Rules List with the click of a button. This is true for both individual hands and
the n-Card exceptions.
Forced Strategy
The forced strategy is described earlier in Chapter 4 however 2 new options are available once the results
have been calculated. The first is to simply recalculate the strategy. This lets you make whatever changes
you like and see what happens without having to redo all of the base calculations.
The second option is one my favorite recent addition, it’s the n-Card Strategy calculation. If you press this
button, then an n-Card strategy will automatically be determined based on all of the current EVs. The
Forced Strategy Table in the Strategies tab will now list the deviation point and if you highlight the box will
also tell you the secondary strategy. The n-Card strategy is always calculated with a base 2-Card strategy so
2 card hands will always be played CD. The weighted average of the like-total 2-card hands will be used
however to determine the base strategy. Beware that if you recalculate the forced strategy all of the indices
will be erased from the table.
11
Effects of Removal (EORs)
The first thing you want to make sure before you press the button is whether or not you want a single EOR
calculated or if you want all of them calculated.
This is another place where the forced strategy is very important. When EORs are calculated the only
strategy that remains fixed is the forced strategy. The TD and CD strategies are optimally calculated given
the card removed. The only exceptions to the fixed forced strategy occur when double down rescue,
redoubling and suited bonuses are allowed because these are always played optimally even after the card is
removed. If you wish to calculate fixed strategy EORs for TD lay simply calculate or copy the TD strategy
to the forced strategy and then calculate the EORs. If you wish to keep the CD strategy fixed, set the forced
strategy to be 21-card CD and then calculate the EORs.
The EORs on the summary page are simply New EV – Original EV.
When looking at each hand individually as well as for the hand total analysis, the EOR given is a simple
Change in EV for Current Strategy + Change in EV for next best current strategy. They are not normalized.
12
Chapter 9
Features List
Features
OBO/ENHC/BB+1/Australian OBBO/Literal OBBO
S17/H17
Finite/Infinite Regular/Spanish/Custom Shoes
DOA/D9/D10/Custom Doubles Allowed
DAN
DAS
Double Soft as Hard (All or just 19)
Double Down Rescue (Early/Late) (pre/post-split)
Redoubling (pre/post-split)
LS/ES/ES10/Custom
Surrender value assignable
SAN
Surrender bonus vs BJ
5 card surrender (2-10/A)
Surrender after split
Resplit/Hit/Double/Surrender split Aces
BJ bonus after split (A/10)
SPlit all pairs or cutsom pairs (e.g. no 4's in England)
CDZ/CDP/CDPN composition split calculations
Player 21 always wins including after double
Custom Player-Dealer ties 17/18/19/20/21/BJ
Custom BJ Pays
Suited BJ Bonuses
User Defined Bonuses based on:
Generic Total/Specific Hands
Upcard
Suited
13
Chapter 9
Acknowledgements
This CA would not have been possible without extensive help from Cacarulo who figured out the finite deck
split calculations with me and was the first to get the numbers and confirm the theory, and who provided
countless sims and helped me with many reference numbers. Thanks also to Steve Jacobs who explained the
various ways hands could be split that helped with the split calculations and Eric Farmer and Stewart Ethier
who found a two other whole sets of equivalent equations for splits and Eric for his CA which gave more
reference numbers. Thanks to the entire Blackjack Workshop crew for their multiple insights and
acceptance. Thanks to Ken Smith and T Hopper for their brute force numbers. Thanks also to Don
Schlesinger for his book and Mike Shackelford for his great website with its multiple game references and
interesting problems which introduced me to the fun world of mathematics of gambling. Thanks to Katarina
Walker for help with sims and explanations for Spanish 21 related numbers that helped with very confusing
bonus rules and Zenfighter for pointing out the problem with my EOR strategies changing that I fixed.
Thanks also to Magician for his recommendations regarding the user-interface. Finally, thanks to my family
for their patience with my programming.
