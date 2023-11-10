let previous = document.querySelector('#pre')
let play = document.querySelector('#play')
let next = document.querySelector('#next')
let title = document.querySelector('#title')
let recent_volume = document.querySelector('#volume')
let volume_show = document.querySelector('#volume_show')
let slider = document.querySelector('#duration_slider')
let show_duration = document.querySelector('#show_duration')
let track_image = document.querySelector('#track_image')
let auto_play = document.querySelector('#auto')
let present = document.querySelector('#present')
let total = document.querySelector('#total')
let artist = document.querySelector('#artist')
let loop = document.querySelector('#loop')
let background = document.querySelector('#day-night')
let html = document.querySelector('html')
let timer
let autoplay = 0;

let index_no = 0
let playing_song = false
let loop_index = index_no
let track = document.createElement('audio')
let All_song = [
  {
    name: "Bye Bye Bye",
        path: "../../wwwroot/music/Bye Bye Bye.mp3",
        img: "../../wwwroot/images/Bye-Bye-Bye.jpg",
    singer: "Lovestoned"
  },
  {
    name: "Under_Over",
      path: "../../wwwroot/music/Under _ Over.mp3",
      img: "../../wwwroot/images/Under_Over.jpg",
    singer: "Gracie Abrams"
  },
  {
    name: "Moshi Moshi",
      path: "../../wwwroot/music/Moshi Moshi.mp3",
      img: "../../wwwroot/images/Moshi-Moshi.jpg",
    singer: "Poppy"
  },
  {
    name: "It can be rotten",
      path: "../../wwwroot/music/可以摆烂咯.mp3",
      img: "../../wwwroot/images/可以摆烂咯.jpg",
    singer: "Dongru Zhuang"
  },
  {
    name: "Zero",
      path: "../../wwwroot/music/零点.mp3",
      img: "../../wwwroot/images/零点.jpg",
    singer: "KeyKey"
  },
  {
    name: "凄美地",
      path: "../../wwwroot/music/凄美地.mp3",
      img: "../../wwwroot/images/凄美地.jpg",
    singer: "Ding Guo"
  },
  {
    name: "Sole",
      path: "../../wwwroot/music/唯一.mp3",
      img: "../../wwwroot/images/唯一.jpg",
    singer: "Accusefive"
  }
]

//All function
function load_track(index_no){
  clearInterval(timer)
  reset_slider()
  track.src = All_song[index_no].path
  title.innerHTML = All_song[index_no].name
  track_image.src = All_song[index_no].img
  artist.innerHTML = All_song[index_no].singer
  track.load()

  total.innerHTML = All_song.length
  present.innerHTML = index_no + 1
  timer = setInterval(range_slider,1000)

}
load_track(index_no)

//mute sound
function mute_sound(){
  track.volume = 0
  volume.value = 0
  volume_show.innerHTML = 0
}


//reset song slider
function reset_slider() {
  slider.value = 0
}


function justplay() {
    if (playing_song == false){
      playsong()
    }else {
      pausesong()
    }
}

function playsong() {
  track.play()
  playing_song = true
  play.innerHTML = '<i class="fa fa-pause"></i>'
}

function pausesong() {
  track.pause()
  playing_song = false
  play.innerHTML = '<i class="fa fa-play"></i>'
}

function next_song() {
    if (index_no < All_song.length - 1){
      index_no += 1
      load_track(index_no)
      playsong()
    }else {
      index_no = 0
      load_track(index_no)
      playsong()
    }


}
function previous_song() {
    if (index_no > 0){
      index_no -= 1
      load_track(index_no)
      playsong()
    }else {
      index_no = All_song.length - 1
      load_track(index_no)
      playsong()
    }


}

function volume_change() {
  volume_show.innerHTML = recent_volume.value
  track.volume = recent_volume.value / 100
}

function change_duration() {
  slider_position = track.duration * (slider.value / 100)
  track.currentTime = slider_position
}

//autoplay fuction
function autoplay_switch() {
  if (autoplay==1){
    autoplay = 0
    auto_play.style.background = "rgba(255,255,255,0.2)"
  }else {
    autoplay = 1
    auto_play.style.background  = "#FF8A65"
  }
}


function range_slider() {
  let position = 0
  //update slider position
  if (!isNaN(track.duration)){
    position = track.currentTime * (100 / track.duration)
    slider.value = position
  }
  if (track.ended){
    play.innerHTML = '<i class="fa fa-play"></i>'
    if (loop.classList.contains('active')) {
      load_track(loop_index)
      playsong()
    }
    if (autoplay == 1){
      index_no += 1
      load_track(index_no)
      playsong()
    }
  }
}
loop.addEventListener('click', loop_song)

function loop_song() {

  if (loop.classList.contains('active')) {
    background.innerHTML = '<i className="fa fa-sun-o" aria-hidden="true"></i>'
    loop.classList.remove('active')
  } else {
    background.innerHTML = '<i className="fa fa-moon-o" aria-hidden="true"></i>'
    loop.classList.add('active')
  }
}
background.classList.add('active')
background.addEventListener('click',function () {
  if (background.classList.contains('active')) {

      html.style.background = "url('../../wwwroot/images/music-bg.gif')"
    html.style.backgroundRepeat = "no-repeat"
    html.style.backgroundSize = "cover"

    background.innerHTML = '<i class="fa fa-sun-o" aria-hidden="true"></i>'
    background.classList.remove('active')

  } else {
      html.style.background = "url('../../wwwroot/images/music-day.jpg')"
    html.style.backgroundRepeat = "no-repeat"
    html.style.backgroundSize = "cover"

    background.innerHTML = '<i class="fa fa-moon-o" aria-hidden="true"></i>'
    background.classList.add('active')

  }
})


