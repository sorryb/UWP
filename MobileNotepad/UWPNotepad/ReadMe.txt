
cd "C:\Program Files (x86)\Windows Kits\10\bin\x86\"
WinAppDeployCmd.exe devices

WinAppDeployCmd install -file "G:\Kits\Kit DotNET\Windows 10\MyWork\UWPNotepad\UWPNotepad\AppPackages\UWPNotepad_1.0.0.0_ARM_Debug_Test\UWPNotepad_1.0.0.0_ARM_Debug.appx" -ip 127.0.0.1 -pin r7t4Z2








Sample code from MVA course :Building Blocks: Universal Windows Platform (https://mva.microsoft.com/en-US/training-courses/building-blocks-universal-windows-platform-16064?l=DsmNM1kDC_206218949)


Other resources :
-dev.windows.com for Documentation
-nuget search after 'Windows 10 UWP'
-github.com/Microsoft/Windows-universal-samples for Microsoft Samples




















Caption:


00:00:04	We've covered a fairbit of ground.
00:00:07	By the way, real quick,this is Building Blocks,
00:00:09	that's Jerry Nixon andI'm Christopher Harrison.
00:00:11	>> And this isMicrosoft Virtual Academy,
00:00:13	where there's a catalogof things to learn.
00:00:15	>> There is, there is, and
00:00:16	we've actually beenlearning a fair bit.
00:00:18	And we've been learning,
00:00:19	among other things, we're justgonna keep going with this.
00:00:23	We've learned how to getstarted creating a UWP app,
00:00:26	a little bit about XAML,a little bit about data binding,
00:00:30	a little bit aboutresponding to device sizes.
00:00:33	But how about capabilities andtaking advantage of some of
00:00:36	those cool littleadvanced features?
00:00:38	How about that?
00:00:39	>> I'll tell you what'simportant too is,
00:00:40	you're on Windows.
00:00:41	There are signatureexperiences that are unique to
00:00:43	Windows as well.
00:00:45	You want your application tobe able to take advantage of
00:00:47	those, not just the specialtythings that are just throughout
00:00:51	the application.
00:00:52	You want to feellike a Windows app.
00:00:53	Let's talk about what someof those things will be.
00:00:55	But before we do, let's alsoremember that across all apps,
00:00:59	there are certain thingsthat need to be in common.
00:01:02	One is the idea ofusage scenarios.
00:01:04	This is, depending onthe type of device you have,
00:01:06	it's used in different ways.
00:01:08	Is the user,do they have a glove on?
00:01:09	Is this always in bright light?
00:01:11	Is this always going to bein a low latency network?
00:01:15	So you get to keep track ofthat, think of that, whether or
00:01:18	not that makes sense todesign your application
00:01:21	to account for it.
00:01:22	Also, this idea ofinput modality.
00:01:24	We support pin inside Windows.
00:01:26	We support touch andmulti-touch inside Windows.
00:01:28	But we also supportthings like gestures.
00:01:31	And we also support the camera.
00:01:33	There are so many thingsthat are at your disposal.
00:01:35	Don't forget to takeadvantage of the suite that's
00:01:38	really at your commandas a developer.
00:01:40	Your imagination is sort ofthe gateway to what you're
00:01:43	able to do.
00:01:44	More importantly, perhapsthan anything on this list so
00:01:47	far, is localization.
00:01:49	I'm not the only person inthe world, and I speak English,
00:01:53	but not everybody speaksEnglish, Christopher.
00:01:55	>> Really?>> Yeah.
00:01:56	>> Interesting.
00:01:57	>> Totally true.
00:01:57	>> A little fun fact for you.
00:02:00	>> Thanks.
00:02:01	Boy, so localization means Ican take my application, and
00:02:04	I can change its strings, right,
00:02:06	so the text is ina different language, but
00:02:08	also change the imagery so it'salso in a different language.
00:02:11	But we also have thisidea of globalization.
00:02:13	Globalization says my images andstrings aren't enough,
00:02:17	my metaphors sometimes need tochange as well because the way
00:02:20	I talk about how to dosomething is appropriate for
00:02:24	maybe North America.
00:02:25	But it is absolutelynot appropriate for
00:02:27	another country andanother culture.
00:02:29	So we have this idea oflocalization for strings.
00:02:31	But we also have this idea ofglobalization for concepts and
00:02:34	implementation as well.
00:02:35	And then finally, and thisis the most important one on
00:02:38	the list,is the idea of accessibility.
00:02:40	Not everybody hasthe same vision.
00:02:42	Not everybody has the sameability to use their hands.
00:02:45	Your application is meant forall their users.
00:02:49	What are your users' situations,and
00:02:51	how can you tailor it to them?
00:02:53	Is it a lot better for you towrite an application that can be
00:02:58	read by a screen reader,of course it is.
00:03:00	You need to put thatin your mind as well.
00:03:02	So these aren'tcapabilities of Windows,
00:03:04	although they're all there.
00:03:05	These are reallycapabilities that go back to
00:03:07	your application, you asa developer implementing them.
00:03:09	So let's talk about what isa capability of Windows that we
00:03:13	can hit as well.
00:03:14	Let's start withapplication lifetime.
00:03:16	The life cycle of yourapplication is unique in Windows
00:03:19	in that it has threepossible states,
00:03:21	it is not always running.
00:03:22	It's on and off,that's maybe a WPF concept, but
00:03:26	here we also havethe idea of suspended.
00:03:29	So this is where becauseof all kinds of reasons,
00:03:31	maybe the user'sanswering their phone.
00:03:33	Maybe the user hasminimized your application.
00:03:36	You move from the running stateinto the suspended state, and
00:03:38	we have transition eventslike suspending, or
00:03:41	resuming as we comeback out of it.
00:03:45	Perhaps the best way to thinkabout this is just a look
00:03:48	at the life cycle.
00:03:49	As your application isactivated, it takes up memory,
00:03:53	and it's running.
00:03:54	And then, all of a sudden,the user for whatever reason,
00:03:56	invokes something that causesit to be suspended, right,
00:03:59	just like answering the phone.
00:04:01	And so, when that happens, yourapplication has five seconds to
00:04:03	take care of business andit goes down.
00:04:05	And then the user hangs up thephone, and you're running again.
00:04:08	Well, that's terrific,you're reactivated.
00:04:10	And then your applicationsare running like normal, and
00:04:12	then you're suspended again.
00:04:14	But this time, the reason you'resuspended is because the user
00:04:17	went to go play a game, a gamethat uses a lot of memory, and
00:04:20	there's not enough resources for
00:04:21	your application to remain inmemory in a suspended state.
00:04:24	And so you get terminated.
00:04:26	You as a developer then get tohandle these three states, and
00:04:28	you need to think aboutthese three states as well.
00:04:31	What if I get terminated all ofa sudden, what am I going to do?
00:04:34	Will the user lose their data,
00:04:35	will the user lose their placein their navigation stack?
00:04:38	Will the user lose theirplace in the scroll position?
00:04:42	Whatever it is thatyour application does,
00:04:45	you get to take careof it by listening for
00:04:47	that suspending state andsaving state, however it is, and
00:04:50	then responding tothat resuming state.
00:04:52	So that's one special thingthat's brilliant inside
00:04:55	the Windows Ecosystem, and
00:04:56	that's the idea ofbeing suspended.
00:04:58	Another thing we have isthe idea of being able to run
00:05:02	even when we're suspended.
00:05:03	That's right.You can still do it.
00:05:04	And we do that withbackground tasks.
00:05:07	Background tasks are definedwith two things.
00:05:09	One is a trigger thatcauses them to happen.
00:05:11	Sometimes this is only time.
00:05:13	Sometimes it's whenthe user logs in.
00:05:14	And there's dozens of triggersthat can cause it to happen.
00:05:17	Then we add on one ormore conditions.
00:05:20	So yeah, time passes,15 minutes, let's say, passes.
00:05:23	My background triggeris invoked, and
00:05:25	when it does, I only want itto run with a condition of
00:05:28	I have Internetaccess at the time.
00:05:30	So, and the user is logged in,
00:05:32	and whatever otherconditions matter.
00:05:34	So there's dozens of each, andI mixed these up depending on
00:05:37	the right usage scenario formy background task.
00:05:40	And it executes without a UI,and
00:05:42	I can do things like updatetiles and things like that.
00:05:44	It's really a terrific way.
00:05:45	And so we can host this sort ofprocess either, or this sort of
00:05:49	task, inside the process memoryof my foreground application, or
00:05:54	inside a dedicated host,right, so it's up to you.
00:05:57	If you put it in the foregroundtask, that's great because it's
00:06:00	shared memory, and I can passthings back and forth between my
00:06:03	foreground application andmy background application.
00:06:05	The downside is I closemy foreground app,
00:06:07	my background taskis also terminated,
00:06:09	so you may not havethe experience that you want.
00:06:12	Again, this istotally up to you.
00:06:14	We have two types of tasks,one that's normal, so
00:06:17	we have 25 seconds toexecute and we're done.
00:06:19	These are wall clock,not CPU time, but wall clock,
00:06:22	25 seconds to go.
00:06:23	And then we have long runningtasks that basically can take
00:06:26	all the time they want,
00:06:27	unless resource constraintsrequire them to be terminated.
00:06:33	So there's two types,the normal 25 second task or
00:06:37	the long running task.
00:06:38	And finally,we have constraints around it.
00:06:40	So the first thing is, I don'thave CPU time constraints, and
00:06:43	I am guaranteed 10%of CPU utilization.
00:06:47	Which means,if you add it all together,
00:06:49	you can never havemore than ten,
00:06:50	but the real rule is eight, youcan only have eight simultaneous
00:06:53	background tasksexecuting at a time,
00:06:54	so everybody gets their 10%share of the CPU utilization.
00:06:58	But, we do have otherconstraints, things like memory,
00:07:03	or network usage.
00:07:04	But again, those are gonnabe driven by the device
00:07:07	that you're on.
00:07:08	Sometimes memory isjust not a big deal,
00:07:09	cuz you're running on a desktop.
00:07:10	Sometimes network usageis just not a big deal,
00:07:13	cuz you're on WiFi, right?
00:07:14	So, those sorts of thingsare all in there, the hard and
00:07:16	fast rules are that25 second wall clock
00:07:19	that you have tokind of run against.
00:07:21	Then there are userconstraints as well.
00:07:22	Your background task can only doso much if the user goes in and
00:07:25	sets like battery saver mode on.
00:07:27	Or during quiet hours, right,and so quiet hours means at
00:07:30	nighttime, I don't geta bunch of notifications but
00:07:32	your background tasks sendsnotifications, pretty reliable
00:07:35	that in quiet hours I'mnot going to receive it.
00:07:37	So, you get to account forthose as well.
00:07:40	You can test for
00:07:40	all of this to make sure yourbackground task is resilient.
00:07:44	Another thing that is iconic,
00:07:45	I would say, and the experienceinside of WIndows 10 is
00:07:50	the idea of live tilesinside my start menu.
00:07:54	You create live tiles.
00:07:56	That's where real information,
00:07:57	not just an icon, not justan icon with numbers, but
00:08:00	real information thatgets pushed down to me.
00:08:02	And so there are many predefinedtemplates I can use, the lay out
00:08:07	my tile, as well as adaptivetiles that allow me to basically
00:08:11	have an open canvas to createany kind of template I want.
00:08:14	There are four template, fourstyles of tiles that I can have,
00:08:19	those are optional, right?
00:08:21	I can just support two if I wantto, or I can support all four.
00:08:24	And if you look at the bottomhere, this is not three tiles.
00:08:29	This is one tile acrossdifferent screen densities.
00:08:32	So as I go from one monitor to areally, really awesome monitor,
00:08:35	I actually have more spaceto show more content.
00:08:38	And so I can write adaptivetiles to show more content,
00:08:41	depending on the density of thescreen they've been written in.
00:08:44	So again, I give you this ideaof a live tile so that your
00:08:47	application has more benefit andmore value to the user.
00:08:50	Then you have tons of morecapability behind the scenes to
00:08:54	make it even cooler,even better, and
00:08:56	still be able to deliveran awesome experience
00:08:59	regardless of the type of deviceyour screen that it's on.
00:09:02	Finally, with live tiles,
00:09:03	there's also secondary tileswhich is absolutely genius.
00:09:06	It's my ability to bypassthe main screen and
00:09:09	go to a specific record.
00:09:11	Let's say your applicationis about mortgages.
00:09:14	And so I can list allthe mortgages on my main page,
00:09:17	drill into a mortgage.
00:09:18	But then I can pin a secondarytile to my start screen
00:09:21	that allows me to bypass allthat searching and go straight
00:09:24	to that mortgage becausethat's the one I'm working on.
00:09:26	That's what a secondary tile is,pretty genius.
00:09:29	Another piece thatreally is part and
00:09:30	parcel to tiles istoast notifications.
00:09:34	Toast notifications are atransient UI that comes and
00:09:37	goes, and more importantly, it'ssomething that can be stopped
00:09:40	by the user, they candisable those notifications.
00:09:43	So it's up to you howyou're gonna use these and
00:09:45	how reliant you'regoing to be on them.
00:09:47	You can update them in threedifferent ways, you can schedule
00:09:50	them, you can update themlocally kind of on the fly.
00:09:53	I'm sorry, you can presentthem locally on the fly, or
00:09:56	you can push them overpush notifications across
00:09:59	the Internet.
00:10:00	We introduced in Windows 10the idea of actionable toast.
00:10:04	Actionable toast means you cansee the one at the top there is
00:10:06	just toast likeyou would expect.
00:10:08	The one at the bottom isactionable toast where you
00:10:10	can add things like formfields and buttons.
00:10:13	And when they click thosebuttons they can call
00:10:16	into your application assome sort of invocation for
00:10:19	your foreground app orthey can actually call into your
00:10:21	background task as well, no UIat all, and everything is done.
00:10:25	So it's a brilliant wayto interact with the user
00:10:28	without having to launchyour application,
00:10:29	but still use the logicof your application.
00:10:32	Now, finally,there's the action center,
00:10:34	the action center thatslides in from the side.
00:10:36	That's where we archive all ofthe toast notifications, so
00:10:41	that's great.
00:10:41	There's a full API aroundthe action center as well for
00:10:43	me to look and see what's thereor remove things, add things.
00:10:46	It's really terrific, and
00:10:47	so this is the idea oftoast notifications.
00:10:50	I can pop things up.
00:10:51	I can make that pop up really,really rich, and interactive for
00:10:55	the user, and it's archivedoff into the action
00:10:58	center that I can interact with,as well.
00:11:01	These sorts of featuressometimes are difficult to
00:11:04	debug because they don'thappen necessarily while
00:11:08	your application is running.
00:11:09	And so inside Visual Studio,how do we do that?
00:11:11	It's easy.
00:11:12	All you have to do is go intothe project properties and
00:11:14	go to the Debug tab.
00:11:16	There's a new check boxthat says, Do not launch,
00:11:19	but debug my codewhen it starts.
00:11:21	So you basically check thatcheck box and go ahead and
00:11:23	debug your application,nothing happens.
00:11:25	Visual Studio just waits.
00:11:27	Then your backgroundtask get's invoked or
00:11:30	a toast notification pops up orsomething that doesn't have a UI
00:11:34	occurs, all of a suddenVisual Studio comes to life and
00:11:36	you're able to stepthrough your code.
00:11:38	It's really an easy way todo what seems to be a really
00:11:41	complicated task.
00:11:43	All right, sowhat do we wanna do?
00:11:45	Let's take a look atour goal for this demo.
00:11:47	I want to enabletoast notifications.
00:11:50	I basically, when the user goesin and they click on the save
00:11:54	button, I just want to give thema nice little alert that says,
00:11:57	we've successfullysaved your file.
00:12:00	That's it, right?
00:12:00	And so of course you would makethis as rich as possible for
00:12:03	your application, too.
00:12:04	All right, this is just aroundtoast notifications, and let me,
00:12:09	if it's all right with you,
00:12:10	Christopher, I'm gonna switchover to Visual Studio.
00:12:12	>> It's just fine.
00:12:13	Switch over to Visual Studio.
00:12:14	You have my permission.
00:12:15	>> Great, great, great.
00:12:16	>> It makes itself.>> This is where we left off,
00:12:18	I just wanna kinda pointout what's going on here.
00:12:20	The applicationruns like normal.
00:12:22	I can go in, I can openthe file like this and
00:12:26	when I click save no matterhow many times I click it,
00:12:28	it is successfully saving, westill have our adaptive design,
00:12:34	it's successfully saving, butit's not telling me anything.
00:12:37	So let's go in and implementtoast notification and we're
00:12:41	gonna do that with a servicebecause we try and make utility
00:12:44	classes so we don't put a bunchof logic inside our ViewModel.
00:12:47	This is our ToastService and so
00:12:50	we'll just implement theToastService and I want to use
00:12:54	a package that's in NuGet thisis made by the Windows team,
00:12:57	the toast notification team.
00:12:59	And so if I go into NuGet and
00:13:00	I search for notificationextensions, there it is.
00:13:03	Look at that,7,000 downloads already.
00:13:05	This is the official libraryfrom Microsoft, and so
00:13:08	this makes it a lot easier forme to not have to create raw
00:13:11	XML, which is ultimatelyhow you define these.
00:13:15	So I'll just create a singlemethod inside this ToastService,
00:13:18	and I'll just drag that in,because it's fully implemented.
00:13:21	There's a lot of syntax here.
00:13:22	This is not aboutthe implementation, but
00:13:25	how I'm going tomake things happen.
00:13:26	So this is just ShowToast,I pass in my model, which is my
00:13:30	file info, and then any specialmessage that I want to have.
00:13:34	And soI'll default it to Success, but
00:13:36	we won't use it that way.
00:13:37	Again, all this logic isabstracted away from my
00:13:40	ViewModel, so that my ViewModeldoesn't have all that logic.
00:13:42	In the end, I'm just usingthe Windows UI notification name
00:13:46	space, getting the XML that'sgenerated by my NuGet package,
00:13:49	and I'm showing it.
00:13:50	It's that's simple.
00:13:51	I say it's that simple.
00:13:52	But we'll provide thiscode to all of you guys so
00:13:54	that you can do it too.
00:13:56	All right, so let's implementit in our ViewModel.
00:13:58	So we're gonna do it everytime it saves, remember that?
00:14:00	So we're using the FileService,but here's what we'll do,
00:14:02	we'll delete the Save method and
00:14:04	put in our own new Save methodthat has this additional step.
00:14:09	So first we make a referenceto our ToastService, right,
00:14:12	so this is where the logic is,so there it is, ToastService.
00:14:15	And now we have twosteps inside our Save.
00:14:17	One, it's inside a try,so one it's going to be,
00:14:20	when it successfully saves,I'm gonna go ahead and
00:14:22	show a toast that says,File successfully saved.
00:14:26	Now, if it doesn't andwe go into the catch block for
00:14:28	whatever reason,I'm still gonna ShowToast.
00:14:31	In this case,I'm gonna say, Save failed,
00:14:32	and give them whateverthe message is.
00:14:34	Here in just a second, I'll showyou how it successfully saves.
00:14:38	And you'll just have to trust methat the Fail method works fine.
00:14:41	>> [LAUGH]>> [LAUGH] You're
00:14:43	going to want to test ityourself of course, but anyway,
00:14:45	this is the idea, right?
00:14:46	Simple kind of work here.
00:14:48	You're going to ShowToast fora lot of different reasons.
00:14:51	And sohere we can abstract it away.
00:14:52	All right, soI'm going to open up,
00:14:53	this time I'm going to openup the Pearl Harbor address.
00:14:56	And it's time that we save itand what when do, there it is.
00:14:59	A little toast notificationpops up, it says,
00:15:01	File saved successfully.
00:15:03	That's my custom message andthen it has the name of the file
00:15:06	that I passed intothat method as well.
00:15:08	And so I clicked ita couple of times, so
00:15:10	you saw a lot of themsort of queued up.
00:15:12	I'm clicking, clicking,clicking, but I don't see them.
00:15:14	But they all get archived forme in the actions center.
00:15:17	Click a few more,re-open the action center,
00:15:19	you can see I haveeven more here.
00:15:20	In fact, here's a cool thing.
00:15:22	This little chevron,if in case it wraps,
00:15:24	it allows me to see moreinformation that way as well.
00:15:26	So it doesn't have tobe a short string.
00:15:28	The simple functionality,
00:15:30	this is very iconic to Windows,this little toast coming in,
00:15:36	defined by XML justlike we do live tiles.
00:15:38	So you can build it all out inXML your own way, or you can use
00:15:43	the extension like I showedthat comes right out of NuGet,
00:15:46	written by the team.
00:15:47	It's terrific, actually.
00:15:48	So I totally recommend that.
00:15:49	We've put that outinto a service, so
00:15:51	it's abstracted away, and
00:15:52	it's just another way ofleveraging what's already there.
00:15:55	I didn't have to build toastinto Windows, I didn't have to
00:15:58	build live tiles into Windows,those things just come for free.
00:16:01	I wanna make sure I make themost of being inside the Windows
00:16:05	ecosystem in the first place.
00:16:06	So, we talked aboutthe application life cycle and
00:16:08	how that's special insideWindows because of
00:16:11	the suspension and
00:16:12	the termination in coming backfrom it, when you resumed.
00:16:15	We also talked about backgroundtasks, and the idea that we can
00:16:18	still run, even though ourforeground app isn't running.
00:16:21	And we have triggersas well as conditions,
00:16:23	to keep those running orto run at specific times.
00:16:26	And then we have whatI say is Life tiles,
00:16:29	I'm a littledisappointed in my type.
00:16:31	I'm just fixing it right now.
00:16:32	>> [LAUGH] Your editor's fired.
00:16:34	>> Live tiles,that's exactly right.
00:16:37	Let's get that,no somethings not right.
00:16:39	There's a percentsign all of a sudden.
00:16:41	There it is.
00:16:42	>> There we go.
00:16:42	>> Live tiles as wellas Toast notification,
00:16:45	all that I need isVisual Studio and I'm done.
00:16:48	>> And you know,I have to throw this in,
00:16:50	I know this is a slight tangent,but it's something that I really
00:16:53	like to point outwith live tiles.
00:16:55	One of the things that youcan actually do if you're
00:16:57	creating a website and
00:16:59	you have some informationthat you want to expose out,
00:17:02	you can actually create that XMLthat you were talking about.
00:17:05	And basically, you register thatinside of a couple of metatags
00:17:09	on your page saying, hey, look,go over here for for the XML.
00:17:12	And what will then happen, isthrough a little bit of polling,
00:17:16	if somebody decided to save theshortcut to their start menu or
00:17:21	to their phone for
00:17:22	example, it will actuallybehave just like a live tile.
00:17:25	So that periodically everycouple of hours it will go back,
00:17:29	it will look at the XML file,if anything's been updated
00:17:32	then that will alsoreflect on the live tile.
00:17:34	So it's not even just forWindows,
00:17:35	something I alwayslike to point out.
00:17:37	>> Yeah.>> And I would mention that
00:17:38	Jeremy Foster and I actually did
00:17:40	a NVA on creating mobile webapps where we show that off.
00:17:44	>> It's almost like fav icon,to the next level.
00:17:46	>> Yeah, exactly.
00:17:47	That's exactly what it is.
00:17:48	Anyway, we covered a fairbit of ground there.
00:17:51	But we,according to the script here,
00:17:53	have one more module to do.
00:17:55	And well, let's go ahead andclose this off and
00:17:57	we'll come back and [CROSSTALK]>> No let's just go
00:17:59	right into it.
00:18:00	>> You're gonnago right into it?
00:18:01	>> I think theseare the resources that every
00:18:03	developer needs.
00:18:04	>> All right, then do it.
00:18:05	Don't let me get in your way.
00:18:06	>> There are three main placesyou can go to really get
00:18:08	a lot of information.
00:18:09	And let's start withdev.windows.com.
00:18:12	That is the developer center,
00:18:14	Microsoft's produceddeveloper center.
00:18:17	Tons of guidance,
00:18:18	all the API documentationon MSDN is from here,
00:18:22	as well as downloads, resources,access to Visual Studio,
00:18:25	everything you can think of,including an arsenal of samples.
00:18:29	But most of the universalsamples now have been
00:18:31	pushed over to GitHub, which isterrific because you can see
00:18:35	all the code, browse it the wayyou're used to, loan it down,
00:18:37	everything is easy to use.
00:18:39	And soif you get over to GitHub, so
00:18:40	this is Github/Microsoft, right?
00:18:43	You can go there and navigateup to Windows-universal-samples,
00:18:46	where there are not dozens,but I think we're almost
00:18:50	to 50 different samplesyou can go through.
00:18:52	Things all the way downthe Bluetooth stack,
00:18:54	up to XAML and implementingindividual controls.
00:18:57	So tons of awesome thingsyou can get from GitHub.
00:19:00	And finally, I wanna justbring you back to NuGet.
00:19:02	So NuGet is available to everydeveloper, they just released
00:19:07	a note that said theyjust hit 50,000 packages.
00:19:11	So here I'm in,I type Windows 10 UWP.
00:19:13	You can see that there'salready 3,600 packages.
00:19:16	The number one packet, lookat that, Template 10 Library.
00:19:19	>> Yeah.>> [LAUGH] That's beautiful.
00:19:20	That just worked out that way,it's not my fault.
00:19:23	But nonetheless, you wannago get this information.
00:19:26	Nuget is there.
00:19:27	You can go to GitHub andget the samples.
00:19:29	You can go to Dev.Windows.comand get all the documentation.
00:19:32	It's just a wealthof resources for
00:19:36	developers to juststart dipping into.
00:19:39	I didn't want to skip it, Ijust didn't want it to miss it.
00:19:41	>> Yep, fair enough.
00:19:42	Yep, no I like it.
00:19:43	So the last little thing thatI wanna mention is in case you
00:19:46	hadn't picked up on it, our goalof course, was not to try and
00:19:49	teach everything that thereis to know about UWP.
00:19:52	It really was to help youget ready for the workshop.
00:19:55	Hopefully, you enjoythe workshop either live or
00:19:58	on-demand on video.
00:20:00	And if you are looking for morein-depth training on UWP and
00:20:06	frankly, any Microsoftdevelopment topic, but
00:20:09	obviously since weare doing UWP, UWP.
00:20:11	We've got a lot of courses that you can check out
00:20:15	on Microsoft virtual academy.
00:20:17	So with that Jerry,thanks very much for coming in.
00:20:19	>> Yeah, absolutely.
00:20:20	>> This is our little Building Blocks, and
00:20:22	we'll see you in the next MVA.
00:20:24	Go enjoy the workshop. Good luck!