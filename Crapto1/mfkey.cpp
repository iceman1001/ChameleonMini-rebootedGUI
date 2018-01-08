//-----------------------------------------------------------------------------
// Merlok - June 2011
// Roel - Dec 2009
// Unknown author
//
// This code is licensed to you under the terms of the GNU GPL, version 2 or,
// at your option, any later version. See the LICENSE.txt file for the text of
// the license.
//-----------------------------------------------------------------------------
// MIFARE Darkside hack
//-----------------------------------------------------------------------------
#include "Stdafx.h"
#include "mfkey.h"

// recover key from 2 different reader responses on same tag challenge
bool mfkey32(uint32_t cuid, uint32_t nt0, uint32_t nt1, uint32_t nr0, uint32_t ar0, uint32_t nr1, uint32_t ar1, uint64_t *outputkey) {
	struct Crypto1State *s,*t;
	uint64_t outkey = 0;
	uint64_t key = 0;     // recovered key
	bool isSuccess = false;
	uint8_t counter = 0;

	uint32_t p640 = prng_successor(nt0, 64);
	uint32_t p641 = prng_successor(nt1, 64);
	s = lfsr_recovery32(ar0 ^ p640, 0);

	for(t = s; t->odd | t->even; ++t) {
		lfsr_rollback_word(t, 0, 0);
		lfsr_rollback_word(t, nr0, 1);
		lfsr_rollback_word(t, cuid ^ nt0, 0);
		crypto1_get_lfsr(t, &key);
		crypto1_word(t, cuid ^ nt0, 0);
		crypto1_word(t, nr1, 1);
		if (ar1 == (crypto1_word(t, 0, 0) ^ p641)) {
			outkey = key;
			counter++;
			if (counter == 20) break;
		}
	}
	isSuccess = (counter == 1);
	*outputkey = ( isSuccess ) ? outkey : 0;
	crypto1_destroy(s);
	return isSuccess;
}

// recover key from 2 reader responses on 2 different tag challenges
// skip "several found keys".  Only return true if ONE key is found
                 // mfkey32v2 <uid>        <nt>        <nt1>          <nr_0>     <ar_0>         <nr_1>      <ar_1>
bool mfkey32_moebius(uint32_t cuid, uint32_t nt0, uint32_t nt1, uint32_t nr0, uint32_t ar0, uint32_t nr1, uint32_t ar1, uint64_t *outputkey){
	struct Crypto1State *s, *t;
	uint64_t outkey  = 0;
	uint64_t key = 0;			// recovered key
	bool isSuccess = false;
	int counter = 0;
	uint32_t p640 = prng_successor(nt0, 64);
	uint32_t p641 = prng_successor(nt1, 64);
		
	s = lfsr_recovery32(ar0 ^ p640, 0);
  
	for(t = s; t->odd | t->even; ++t) {
		lfsr_rollback_word(t, 0, 0);
		lfsr_rollback_word(t, nr0, 1);
		lfsr_rollback_word(t, cuid ^ nt0, 0);
		crypto1_get_lfsr(t, &key);
		
		crypto1_word(t, cuid ^ nt1, 0);
		crypto1_word(t, nr1, 1);
		if (ar1 == (crypto1_word(t, 0, 0) ^ p641)) {
			outkey = key;
			++counter;
			if (counter == 20) break;
		}
	}
	isSuccess = (counter == 1);
	*outputkey = ( isSuccess ) ? outkey : 0;
	crypto1_destroy(s);
	return isSuccess;
}

// recover key from reader response and tag response of one authentication sequence
void mfkey64(nonces_t data, uint64_t *outputkey){
	uint64_t key = 0;				// recovered key
	uint32_t ks2;    				// keystream used to encrypt reader response
	uint32_t ks3;    				// keystream used to encrypt tag response
	struct Crypto1State *revstate;
	
	// Extract the keystream from the messages
	ks2 = data.ar ^ prng_successor(data.nonce, 64);
	ks3 = data.at ^ prng_successor(data.nonce, 96);
	revstate = lfsr_recovery64(ks2, ks3);
	lfsr_rollback_word(revstate, 0, 0);
	lfsr_rollback_word(revstate, 0, 0);
	lfsr_rollback_word(revstate, data.nr, 1);
	lfsr_rollback_word(revstate, data.cuid ^ data.nonce, 0);
	crypto1_get_lfsr(revstate, &key);
	crypto1_destroy(revstate);
	*outputkey = key;
}
