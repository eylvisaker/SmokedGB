﻿//
//    Copyright (c) 2006-2017 Erik Ylvisaker
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//  
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgateLib.AudioLib 
{
	/// <summary>
	/// Class which can be used to query information about what features are supported
	/// by the audio driver.
	/// </summary>
	public class AudioCapsInfo
	{
		internal AudioCapsInfo()
		{ }

		/// <summary>
		/// Indicates whether or not the application can stream audio to the
		/// audio system by creating a StreamingSoundBuffer.
		/// </summary>
		public bool SupportsStreamingAudio
		{
			get { return Audio.Impl.CapsBool(AudioBoolCaps.StreamingSoundBuffer); }
		}
	}

	/// <summary>
	/// Testable boolean capabilities.
	/// </summary>
	public enum AudioBoolCaps
	{
		/// <summary>
		/// Indicates whether or not the audio driver supports streaming audio
		/// generated by the application.
		/// </summary>
		StreamingSoundBuffer,
	}
}
