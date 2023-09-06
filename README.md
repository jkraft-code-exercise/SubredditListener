# SubredditListener
## Configuration
First, [register a "Script" type app](https://github.com/reddit-archive/reddit/wiki/OAuth2) with Reddit.  Note that `redirect uri` is not important for SubredditLitener and can be set to any value.

In `appsettings.json`, specify values for:
* `Username` -- Your Reddit username
* `Password` -- Your Reddit password
* `ClientId` -- The Client Id from your Reddit app registration
* `ClientSecret` -- The Client Secret from your Reddit app registration

A few `Subreddits` are configured as an example.

For `Logging`, `Information` log level should provide a good summary of what's happening.  Changing to `Debug` will add a few more entries.
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "RedditClient": {    
    "BaseAuthUrl": "https://www.reddit.com",
    "BaseApiUrl": "https://oauth.reddit.com",
    "Username": "YOUR_REDDIT_USERNAME",
    "Password": "YOUR_REDDIT_PASSWORD",
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET",
    "UserAgent": "SubredditListener/0.1 by Level-Pepper-2258",
    "Subreddits": [
      "music",
      "pics",
      "aww",
      "movies",
      "science"
    ],
    "MaxPostsPerRequest": 100,
    "MaxPostsPerSubreddit":  1000
  }
}

```
### Operation
Upon execution, log entries will track which subreddits are being monitored, information about rate limiting, and occasionally display a summary of statistics:
```
info: SubredditListener.RedditClient[0]
      RedditClient created
info: SubredditListener.RedditWorker[0]
      RedditWorker running at: 09/06/2023 15:12:21 -04:00
info: SubredditListener.RedditWorker[0]
      Getting posts for subreddit 'music'
info: SubredditListener.RedditWorker[0]
      Delaying for 1 seconds to stay within rate limit of (null) which resets in (null) seconds
info: SubredditListener.RedditWorker[0]
      Getting posts for subreddit 'pics'
info: SubredditListener.RedditWorker[0]
      Delaying for 1 seconds to stay within rate limit of (null) which resets in (null) seconds
info: SubredditListener.RedditWorker[0]
      Getting posts for subreddit 'aww'
info: SubredditListener.RedditWorker[0]
      Delaying for 1 seconds to stay within rate limit of (null) which resets in (null) seconds
info: SubredditListener.RedditWorker[0]
      Getting posts for subreddit 'movies'
info: SubredditListener.RedditWorker[0]
      Delaying for 1 seconds to stay within rate limit of (null) which resets in (null) seconds
info: SubredditListener.RedditWorker[0]
      Getting posts for subreddit 'science'
info: SubredditListener.RedditWorker[0]
      Delaying for 1 seconds to stay within rate limit of (null) which resets in (null) seconds
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\justi\source\repos\SubredditListener\SubredditListener
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0635 seconds to stay within rate limit of 598 which resets in 458 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0738 seconds to stay within rate limit of 596 which resets in 458 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.079 seconds to stay within rate limit of 595 which resets in 458 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0687 seconds to stay within rate limit of 597 which resets in 458 seconds

[...]

info: SubredditListener.RedditWorker[0]
      Delaying for 3.0429 seconds to stay within rate limit of 560 which resets in 426 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0483 seconds to stay within rate limit of 559 which resets in 426 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0538 seconds to stay within rate limit of 558 which resets in 426 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0521 seconds to stay within rate limit of 557 which resets in 425 seconds
info: SubredditListener.RedditWorker[0]
      Got 942 total posts for subreddit 'music'
info: SubredditListener.RedditWorker[0]
      Got 904 total posts for subreddit 'science'
info: SubredditListener.RedditWorker[0]
      Got 917 total posts for subreddit 'movies'
info: SubredditListener.RedditWorker[0]
      Delaying for 3.036 seconds to stay within rate limit of 556 which resets in 422 seconds
info: SubredditListener.RedditWorker[0]
      Delaying for 3.0435 seconds to stay within rate limit of 552 which resets in 420 seconds
info: SubredditListener.RedditWorker[0]
      Got 965 total posts for subreddit 'pics'
info: SubredditListener.RedditWorker[0]
      Got 902 total posts for subreddit 'aww'
info: SubredditListener.RedditWorker[0]
      Statistics for subreddit "Music" with 942 posts

      Top Posts
      |Ups       |Id        |Author                        |Permalink                                                                                 |
      |     35441|   169uds1|                  DemiFiendRSA|              /r/Music/comments/169uds1/steve_harwell_smash_mouth_founding_singer_dead_at/|
      |     17005|   16971wm|           IonHazzikostasIsGod|              /r/Music/comments/16971wm/smash_mouths_steve_harwell_on_death_bed_only_days/|
      |      8587|   167uz78|                  crotchboxing|                                       /r/Music/comments/167uz78/jimmy_buffett_dead_at_76/|
      |      4388|   16a5fl3|           Interesting-Sun-759|                                          /r/Music/comments/16a5fl3/why_is_beyoncé_so_big/|
      |      3330|   1678fyw|                 nightpanda893|                  /r/Music/comments/1678fyw/dire_straits_guitarist_jack_sonni_dies_age_68/|
      |      2494|   168obku|          Total-Enthusiasm9130|                /r/Music/comments/168obku/anyone_else_think_lana_del_reys_songs_sound_the/|
      |      2493|   16b9jvu|                        Izakei|                                    /r/Music/comments/16b9jvu/whats_the_saddest_song_ever/|
      |      1009|   16apn83|              YoureASkyscraper|              /r/Music/comments/16apn83/nirvana_reissuing_in_utero_with_2_unreleased_live/|
      |       989|   16ado0i|           throwradogscandance|              /r/Music/comments/16ado0i/do_you_know_of_any_artists_or_bands_that_secretly/|
      |       803|   16a7xm6|          MoreThanAFeeling1976|                  /r/Music/comments/16a7xm6/dream_weaver_love_is_alive_singer_gary_wright/|

      Top Users
      |PostCount |AuthorFullName      |Author                        |
      |        67|        t2_c1n7c544u|                     IPersonaI|
      |        17|        t2_g36njy43l|              SnarkAndAcrimony|
      |        12|        t2_dehjhynfv|              PositivelyStrobe|
      |        12|        t2_eqrybm8m8|          Huge-Description-787|
      |        12|         t2_8idiixtf|                      Mcgoo186|
      |         9|           t2_15qgq4|                      d3rk2007|
      |         8|           t2_13ggxe|                  dragonoid296|
      |         6|           t2_13ppet|                    mediazikos|
      |         6|        t2_hxg4co94c|                 ShamanicGuide|
      |         6|            t2_a3iyr|              smilysmilysmooch|
info: SubredditListener.RedditWorker[0]
      Statistics for subreddit "pics" with 965 posts

      Top Posts
      |Ups       |Id        |Author                        |Permalink                                                                                 |
      |     48925|   16841ok|                        etfvpu|                          /r/pics/comments/16841ok/ron_desantis_walks_past_biden_seething/|
      |     34278|   166iscj|                   Pazuzu_____|               /r/pics/comments/166iscj/cave_etching_of_a_nude_woman_believed_to_be_30000/|
      |     33501|   169gi64|                   Jackalscott|               /r/pics/comments/169gi64/found_this_little_girl_on_a_busy_road_in_105_heat/|
      |     32097|   168695i|           SweetSoundOfSilence|                                           /r/pics/comments/168695i/disney_world_just_now/|
      |     28253|   169wy7v|              Agile-Umpire4383|                                                    /r/pics/comments/169wy7v/im_52_hes_78/|
      |     27916|   16a2dgo|             Lifegoesonforever|                  /r/pics/comments/16a2dgo/pilot_dies_after_a_plane_crashes_during_gender/|
      |     27177|   168gk93|                      Bizzyguy|                  /r/pics/comments/168gk93/this_group_is_marching_around_orlando_fl_today/|
      |     26465|   167vxxa|                       egap420|                       /r/pics/comments/167vxxa/pic_right_when_lightning_struck_las_vegas/|
      |     23156|   168igdm|                       lfcinvt|                  /r/pics/comments/168igdm/my_dad_and_me_in_1984_he_passed_last_year_been/|
      |     20791|   16a4lw3|                       bekmoto|               /r/pics/comments/16a4lw3/my_kid_asked_why_this_guys_laptop_didnt_have_keys/|

      Top Users
      |PostCount |AuthorFullName      |Author                        |
      |         7|           t2_15ifmi|               glennmelenhorst|
      |         6|            t2_42en7|                          db82|
      |         6|        t2_imupt0nke|            Yesterdayfreepizza|
      |         6|           t2_10k39a|              OregonTripleBeam|
      |         6|        t2_itjcxbozz|                callmegooddess|
      |         5|         t2_nbsdicmr|                        Dumb40|
      |         5|         t2_bef6qxz2|                  Cerenity1000|
      |         5|            t2_ar3bt|                      megalaks|
      |         4|            t2_ev2y4|                  mhudson42484|
      |         4|         t2_gc5crpb4|                 RyanCooper510|
info: SubredditListener.RedditWorker[0]
      Statistics for subreddit "movies" with 917 posts

      Top Posts
      |Ups       |Id        |Author                        |Permalink                                                                                 |
      |     13564|   16amo2m|                indig0sixalpha|               /r/movies/comments/16amo2m/warner_bros_discovery_says_ongoing_strikes_will/|
      |     11325|   168gmgn|            itcamefromtheimgur|               /r/movies/comments/168gmgn/what_are_some_of_the_worst_movie_takes_you_have/|
      |      8270|   1661jlj|                    lowell2017|                    /r/movies/comments/1661jlj/john_wick_director_on_how_his_henry_cavill/|
      |      8131|   169t998|                 JermaineBell4|              /r/movies/comments/169t998/whats_the_most_captivating_opening_sequence_in_a/|
      |      7989|   164p3m1|                   goliath1515|                               /r/movies/comments/164p3m1/any_you_missed_the_point_movies/|
      |      7539|   165e3g3|            MarvelsGrantMan136|                  /r/movies/comments/165e3g3/new_poster_for_yorgos_lanthimoss_poor_things/|
      |      6141|   169jqo5|                    ICumCoffee|                           /r/movies/comments/169jqo5/godzilla_minus_one_official_trailer/|
      |      5901|   165kczw|                 unitedfan6191|             /r/movies/comments/165kczw/actors_who_have_performed_a_1010_performance_only/|
      |      5863|   16amv9k|                 NewDaysBreath|               /r/movies/comments/16amv9k/what_movies_bombed_but_are_actually_really_good/|
      |      5779|   166sp52|                  nailbiter111|                    /r/movies/comments/166sp52/the_practical_effects_wizardry_of_dungeons/|

      Top Users
      |PostCount |AuthorFullName      |Author                        |
      |        26|            t2_ht9gq|                indig0sixalpha|
      |        18|         t2_gg5mj2pk|          Helloimafanoffiction|
      |        14|        t2_4nelec8j8|               Key_Damage_9220|
      |        13|         t2_9al2smj3|              Chemical-Ad-2694|
      |        12|         t2_5adwlxvn|            MarvelsGrantMan136|
      |        11|         t2_22v2n3mu|                    lowell2017|
      |        11|         t2_9v97hpj0|            Equivalent_Ad_9066|
      |        10|         t2_n3nndu0m|            SavingsService2138|
      |         8|         t2_4twua1jz|                        Fan387|
      |         7|         t2_ukuigivk|             KillerCroc1234567|
info: SubredditListener.RedditWorker[0]
      Statistics for subreddit "aww" with 902 posts

      Top Posts
      |Ups       |Id        |Author                        |Permalink                                                                                 |
      |     54756|   167i9w4|                 NICOLETTE_ANN|                                            /r/aww/comments/167i9w4/youre_now_a_cat_owner/|
      |     44403|   1682i5x|                    vladgrinch|                                         /r/aww/comments/1682i5x/some_things_never_change/|
      |     33973|   1665ysw|             anamazingredditor|                                   /r/aww/comments/1665ysw/mother_opossum_with_her_babies/|
      |     32737|   16amtdv|                 Baskerville84|                                 /r/aww/comments/16amtdv/subzero_and_scorpion_dog_cosplay/|
      |     27713|   165mfc9|             thehornsoffscreen|                                        /r/aww/comments/165mfc9/the_cutest_jumpscare_ever/|
      |     27669|   16a7g97|             anamazingredditor|                                       /r/aww/comments/16a7g97/pair_of_stoats_on_the_road/|
      |     24025|   165dbbz|           Illustrious_Fix2933|                /r/aww/comments/165dbbz/baby_bat_struggling_to_get_a_hold_of_mans_fingers/|
      |     23125|   166x3b6|                   fat_old_boy|                                     /r/aww/comments/166x3b6/bro_successfully_did_the_cat/|
      |     20653|   16abxoe|                     SAVertigo|                /r/aww/comments/16abxoe/update_we_adopted_a_mother_and_son_combo_from_the/|
      |     17756|   164z8qn|              greeneyedgirl626|                      /r/aww/comments/164z8qn/my_old_lady_laci_turns_14_today_she_got_her/|

      Top Users
      |PostCount |AuthorFullName      |Author                        |
      |        21|            t2_4eqhs|                      lnfinity|
      |         9|        t2_83a2a0d2k|                   Castlevoidz|
      |         9|         t2_d8w4e436|                    Modern-Moo|
      |         8|         t2_utn6npa9|                   fat_old_boy|
      |         7|        t2_dk6xb6l9u|            Lazy-Broccoli-3180|
      |         6|            t2_brdku|                 sonia72quebec|
      |         5|        t2_5xoddm0bx|           Illustrious_Fix2933|
      |         4|         t2_t0lurfnj|              Vast_Mobile_1686|
      |         4|         t2_w7o0urh4|                  mushi_shroom|
      |         4|         t2_7sbm6hc4|             thehornsoffscreen|
info: SubredditListener.RedditWorker[0]
      Statistics for subreddit "science" with 904 posts

      Top Posts
      |Ups       |Id        |Author                        |Permalink                                                                                 |
      |     31053|   15uagvb|                  Level-Wasabi|            /r/science/comments/15uagvb/americas_richest_10_are_responsible_for_40_of_its/|
      |     17781|   1677ahd|                          mvea|               /r/science/comments/1677ahd/lose_fat_while_eating_all_you_want_researchers/|
      |     16155|   160v8j9|                     chrisdh79|                    /r/science/comments/160v8j9/a_new_european_study_has_found_that_90_of/|
      |     15898|   15r7nvn|                  FunnyGamer97|             /r/science/comments/15r7nvn/new_research_finds_a_significant_slice_of_the_us/|
      |     14404|   15z0b5g|                     chrisdh79|               /r/science/comments/15z0b5g/waste_coffee_grounds_make_concrete_30_stronger/|
      |     14302|   160fgrd|                    marketrent|                    /r/science/comments/160fgrd/emperor_penguin_colonies_experience_total/|
      |     14256|   15o64av|                          mvea|                   /r/science/comments/15o64av/life_is_harder_for_adolescents_who_are_not/|
      |     12912|   166axfk|                          mvea|             /r/science/comments/166axfk/a_mere_12_of_americans_eat_half_the_nations_beef/|
      |     12849|   15tgxek|                          mvea|                     /r/science/comments/15tgxek/a_projected_93_million_us_adults_who_are/|
      |     11286|   15vdllc|                      Wagamaga|              /r/science/comments/15vdllc/a_new_stanford_study_reveals_how_meat_and_dairy/|

      Top Users
      |PostCount |AuthorFullName      |Author                        |
      |       115|             t2_6hji|                          mvea|
      |        92|            t2_l1ket|               giuliomagnifico|
      |        66|            t2_guf36|                      Wagamaga|
      |        52|         t2_2uwit82z|                     chrisdh79|
      |        41|          t2_yg7avor|                      basmwklz|
      |        39|         t2_2vdtqcmq|                  MistWeaver80|
      |        27|            t2_ogvgu|                  FunnyGamer97|
      |        27|         t2_8qgipx5i|                      Ovaz1088|
      |        11|            t2_6yoba|                 smurfyjenkins|
      |        11|            t2_bt932|               HeinieKaboobler|
```
A simple REST API is exposed at http://localhost:5000/swagger with a few endpoints:
* http://localhost:5000/poststatistics
* http://localhost:5000/posts/{id}