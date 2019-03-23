import sys
import Vokaturi
import ctypes

class vokaNetWrapper:
	def __init__(self, DLLstring):
		Vokaturi.load(DLLstring)

	def vokalculate(self, doubleArr):
		samplerate = 44100
		error = "Starting"
		buffer_size = len(doubleArr)


		c_buffer = Vokaturi.SampleArrayC(buffer_size)
		c_buffer [:] = doubleArr[:]
		
		voice = Vokaturi.Voice (samplerate, buffer_size)
		voice.fill(buffer_size, c_buffer)

		quality = Vokaturi.Quality()
		emotionProbabilities = Vokaturi.EmotionProbabilities()
		voice.extract(quality, emotionProbabilities)

		success = bool(quality.valid)

		if(quality.valid):
			error = error + "\n SUCCESS!"
		else:
			error = error + "\n Not enough sonorancy to determine emotions" 

		return {
		"Neutral": emotionProbabilities.neutrality,
		"Happy": emotionProbabilities.happiness,
		"Sad": emotionProbabilities.sadness,
		"Angry": emotionProbabilities.anger,
		"Fear": emotionProbabilities.fear,
		"Error": error,
		"Success" : success
		}


