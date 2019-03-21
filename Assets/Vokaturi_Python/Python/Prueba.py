import sys
import Vokaturi


class vokaNetWrapper:
	def __init__(self, DLLstring):
		Vokaturi.load(DLLstring)

	def vokalculate(self, floatArray):
		samplerate = 44100
		floatArray = [float (x) for x in floatArray]

		buffer_length = len(floatArray)
		cbuff = Vokaturi.SampleArrayC(buffer_length)

		#if floatArray.ndim == 1:  # mono
		cbuff[:] = floatArray[:] / 32768.0
		#else:  # stereo
			#cbuff[:] = 0.5*(floatArray[:,0]+0.0+floatArray[:,1]) / 32768.0

		voice = Vokaturi.Voice(samplerate, buffer_length)
		voice.fill(buffer_length, cbuff)

		quality = Vokaturi.Quality()
		emotionProbabilities = Vokaturi.EmotionProbabilities()
		voice.extract(quality, emotionProbabilities)

		error = "Success"
		if quality.valid:
			print ("Neutral: %.3f" % emotionProbabilities.neutrality)
			print ("Happy: %.3f" % emotionProbabilities.happiness)
			print ("Sad: %.3f" % emotionProbabilities.sadness)
			print ("Angry: %.3f" % emotionProbabilities.anger)
			print ("Fear: %.3f" % emotionProbabilities.fear)
		else:
			error = "Not enough sonorancy to determine emotions"

		voice.destroy()


		return {
		"Neutral": emotionProbabilities.neutrality,
		"Happy": emotionProbabilities.happiness,
		"Sad": emotionProbabilities.sadness,
		"Angry": emotionProbabilities.anger,
		"Fear": emotionProbabilities.fear,
		"Error": error
		}
