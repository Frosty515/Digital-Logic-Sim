namespace DLS.Simulation
{
	public class PinState
	{
		// Each bit has three possible states (tri-state logic):
		public const ulong LogicLow = 0;
		public const ulong LogicHigh = 1;
		public const ulong LogicDisconnected = 2;

		public readonly int BitCount;

		// LOW/HIGH state of each bit
		ulong bitStates;

		// If flag is set, it means the corresponding bit state is DISCONNECTED
		// (note: corresponding bit state is expected to be set to LOW in that case)
		ulong tristateFlags;

		public PinState(int numBits)
		{
			BitCount = numBits;
		}

		public ulong GetRawBits() => bitStates;
		public ulong GetRawTristateFlags() => tristateFlags;

		public void SetAllBits_NoneDisconnected(ulong newBitStates)
		{
			bitStates = newBitStates;
			tristateFlags = 0;
		}

		public bool FirstBitHigh() => (bitStates & 1) == LogicHigh;

		public void SetBit(int bitIndex, ulong newState)
		{
			// Clear current state
			ulong mask = ~(1ul << bitIndex);
			bitStates &= mask;
			tristateFlags &= mask;

			// Set new state
			bitStates |= (newState & 1ul) << bitIndex;
			tristateFlags |= (newState >> 1) << bitIndex;
		}

		public ulong GetBit(int bitIndex)
		{
			ulong state = (bitStates >> bitIndex) & 1;
			ulong tri = (tristateFlags >> bitIndex) & 1;
			return state | (tri << 1); // Combine to form tri-stated value: 0 = LOW, 1 = HIGH, 2 = DISCONNECTED
		}

		public void SetFromSource(PinState source)
		{
			bitStates = source.bitStates;
			tristateFlags = source.tristateFlags;
		}

		public void Set4BitFrom8BitSource(PinState source8bit, bool firstNibble)
		{
			if (firstNibble)
			{
				const uint mask = 0b1111;
				bitStates = source8bit.bitStates & mask;
				tristateFlags = source8bit.tristateFlags & mask;
			}
			else
			{
				const uint mask = 0b11110000;
				bitStates = (source8bit.bitStates & mask) >> 4;
				tristateFlags = (source8bit.tristateFlags & mask) >> 4;
			}
		}

		public void Set8BitFrom4BitSources(PinState a, PinState b)
		{
			bitStates = a.bitStates | (b.bitStates << 4);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 4);
		}

		public void Set16BitFrom4BitSources(PinState a, PinState b, PinState c, PinState d)
		{
			bitStates = a.bitStates | (b.bitStates << 4) | (c.bitStates << 8) | (d.bitStates << 12);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 4) | (c.tristateFlags << 8) | (d.tristateFlags << 12);
		}

		public void Set32BitFrom4BitSources(PinState a, PinState b, PinState c, PinState d, PinState e, PinState f, PinState g, PinState h)
		{
			bitStates = a.bitStates | (b.bitStates << 4) | (c.bitStates << 8) | (d.bitStates << 12) |
						(e.bitStates << 16) | (f.bitStates << 20) | (g.bitStates << 24) | (h.bitStates << 28);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 4) | (c.tristateFlags << 8) | (d.tristateFlags << 12) |
							(e.tristateFlags << 16) | (f.tristateFlags << 20) | (g.tristateFlags << 24) | (h.tristateFlags << 28);
		}

		public void Set64BitFrom4BitSources(PinState a, PinState b, PinState c, PinState d, PinState e, PinState f, PinState g, PinState h,
			PinState i, PinState j, PinState k, PinState l, PinState m, PinState n, PinState o, PinState p)
		{
			bitStates = a.bitStates | (b.bitStates << 4) | (c.bitStates << 8) | (d.bitStates << 12) |
						(e.bitStates << 16) | (f.bitStates << 20) | (g.bitStates << 24) | (h.bitStates << 28) |
						(i.bitStates << 32) | (j.bitStates << 36) | (k.bitStates << 40) | (l.bitStates << 44) |
						(m.bitStates << 48) | (n.bitStates << 52) | (o.bitStates << 56) | (p.bitStates << 60);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 4) | (c.tristateFlags << 8) | (d.tristateFlags << 12) |
							(e.tristateFlags << 16) | (f.tristateFlags << 20) | (g.tristateFlags << 24) | (h.tristateFlags << 28) |
							(i.tristateFlags << 32) | (j.tristateFlags << 36) | (k.tristateFlags << 40) | (l.tristateFlags << 44) |
							(m.tristateFlags << 48) | (n.tristateFlags << 52) | (o.tristateFlags << 56) | (p.tristateFlags << 60);
		}

		public void Set16BitFrom8BitSources(PinState a, PinState b)
		{
			bitStates = a.bitStates | (b.bitStates << 8);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 8);
		}

		public void Set32BitFrom8BitSources(PinState a, PinState b, PinState c, PinState d)
		{
			bitStates = a.bitStates | (b.bitStates << 8) | (c.bitStates << 16) | (d.bitStates << 24);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 8) | (c.tristateFlags << 16) | (d.tristateFlags << 24);
		}

		public void Set64BitFrom8BitSources(PinState a, PinState b, PinState c, PinState d, PinState e, PinState f, PinState g, PinState h)
		{
			bitStates = a.bitStates | (b.bitStates << 8) | (c.bitStates << 16) | (d.bitStates << 24) |
						(e.bitStates << 32) | (f.bitStates << 40) | (g.bitStates << 48) | (h.bitStates << 56);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 8) | (c.tristateFlags << 16) | (d.tristateFlags << 24) |
							(e.tristateFlags << 32) | (f.tristateFlags << 40) | (g.tristateFlags << 48) | (h.tristateFlags << 56);
		}

		public void Set32BitFrom16BitSources(PinState a, PinState b)
		{
			bitStates = a.bitStates | (b.bitStates << 16);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 16);
		}

		public void Set64BitFrom16BitSources(PinState a, PinState b, PinState c, PinState d)
		{
			bitStates = a.bitStates | (b.bitStates << 16) | (c.bitStates << 32) | (d.bitStates << 48);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 16) | (c.tristateFlags << 32) | (d.tristateFlags << 48);
		}

		public void Set64BitFrom32BitSources(PinState a, PinState b)
		{
			bitStates = a.bitStates | (b.bitStates << 32);
			tristateFlags = a.tristateFlags | (b.tristateFlags << 32);
		}

		public void Set4BitFrom16BitSource(PinState source16bit, int index)
		{
			const uint mask = 0b1111;
			bitStates = (source16bit.bitStates >> (index * 4)) & mask;
			tristateFlags = (source16bit.tristateFlags >> (index * 4)) & mask;
		}

		public void Set4BitFrom32BitSource(PinState source32bit, int index)
		{
			const uint mask = 0b1111;
			bitStates = (source32bit.bitStates >> (index * 4)) & mask;
			tristateFlags = (source32bit.tristateFlags >> (index * 4)) & mask;
		}

		public void Set4BitFrom64BitSource(PinState source64bit, int index)
		{
			const uint mask = 0b1111;
			bitStates = (source64bit.bitStates & mask) >> (index * 4);
			tristateFlags = (source64bit.tristateFlags & mask) >> (index * 4);
		}

		public void Set8BitFrom16BitSource(PinState source16bit, bool firstByte)
		{
			if (firstByte)
			{
				const uint mask = 0b11111111;
				bitStates = source16bit.bitStates & mask;
				tristateFlags = source16bit.tristateFlags & mask;
			}
			else
			{
				const uint mask = 0b1111111100000000;
				bitStates = (source16bit.bitStates & mask) >> 8;
				tristateFlags = (source16bit.tristateFlags & mask) >> 8;
			}
		}

		public void Set8BitFrom32BitSource(PinState source32bit, int index)
		{
			const uint mask = 0b11111111;
			bitStates = (source32bit.bitStates >> (index * 8)) & mask;
			tristateFlags = (source32bit.tristateFlags >> (index * 8)) & mask;
		}

		public void Set8BitFrom64BitSource(PinState source64bit, int index)
		{
			const uint mask = 0b11111111;
			bitStates = (source64bit.bitStates >> (index * 8)) & mask;
			tristateFlags = (source64bit.tristateFlags >> (index * 8)) & mask;
		}

		public void Set16BitFrom32BitSource(PinState source32bit, bool firstWord)
		{
			if (firstWord)
			{
				const uint mask = 0xFFFF;
				bitStates = source32bit.bitStates & mask;
				tristateFlags = source32bit.tristateFlags & mask;
			}
			else
			{
				const uint mask = 0xFFFF0000;
				bitStates = (source32bit.bitStates & mask) >> 16;
				tristateFlags = (source32bit.tristateFlags & mask) >> 16;
			}
		}

		public void Set16BitFrom64BitSource(PinState source64bit, int index)
		{
			const uint mask = 0xFFFF;
			bitStates = (source64bit.bitStates >> (index * 16)) & mask;
			tristateFlags = (source64bit.tristateFlags >> (index * 16)) & mask;
		}

		public void Set32BitFrom64BitSource(PinState source64bit, bool firstDWord)
		{
			if (firstDWord)
			{
				const uint mask = 0xFFFFFFFF;
				bitStates = source64bit.bitStates & mask;
				tristateFlags = source64bit.tristateFlags & mask;
			}
			else
			{
				const ulong mask = 0xFFFFFFFF00000000;
				bitStates = (source64bit.bitStates & mask) >> 32;
				tristateFlags = (source64bit.tristateFlags & mask) >> 32;
			}
		}



		public void Toggle(int bitIndex)
		{
			bitStates ^= 1ul << bitIndex;

			// Clear tristate flag (can't be disconnected if toggling)
			tristateFlags &= ~(1ul << bitIndex);
		}

		public void SetAllDisconnected()
		{
			bitStates = 0;
			if (BitCount == 64)
			{
				tristateFlags = 0xFFFFFFFFFFFFFFFF;
			}
			else
			{
				tristateFlags = (1ul << BitCount) - 1;
			}
		}
	}
}