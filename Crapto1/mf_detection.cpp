#include "crapto1.h"
#include <string>
#include <vector>
#include "Stdafx.h"



	uint64_t mfkey(uint32_t uid, uint32_t nt, uint32_t nt1, uint32_t nr0_enc, uint32_t ar0_enc, uint32_t nr1_enc, uint32_t ar1_enc) {

		struct Crypto1State *s, *t;
		uint64_t key;     // recovered key
		uint32_t ks2;     // keystream used to encrypt reader response

						  // Generate lfsr succesors of the tag challenge

		prng_successor(nt, 64);
		prng_successor(nt, 96);

		ks2 = ar0_enc ^ prng_successor(nt, 64);

		s = lfsr_recovery32(ar0_enc ^ prng_successor(nt, 64), 0);

		for (t = s; t->odd | t->even; ++t) {
			lfsr_rollback_word(t, 0, 0);
			lfsr_rollback_word(t, nr0_enc, 1);
			lfsr_rollback_word(t, uid ^ nt, 0);
			crypto1_get_lfsr(t, &key);
			crypto1_word(t, uid ^ nt1, 0);
			crypto1_word(t, nr1_enc, 1);

			if (ar1_enc == (crypto1_word(t, 0, 0) ^ prng_successor(nt1, 64))) {
				return key;
			}
		}
		free(s);
		return 0xffffffffffffffff;
	}
