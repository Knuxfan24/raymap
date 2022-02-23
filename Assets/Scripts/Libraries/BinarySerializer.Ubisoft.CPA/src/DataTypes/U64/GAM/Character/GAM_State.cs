﻿using System;

namespace BinarySerializer.Ubisoft.CPA.U64 {
	public class GAM_State : U64_Struct {
		public U64_Reference<U64_Placeholder> AnimInfo { get; set; }
		public U64_Reference<U64_Placeholder> MechanicsIdCard { get; set; }
		public U64_Reference<U64_Placeholder> TargetStateList { get; set; }
		public U64_Reference<GAM_State> NextState { get; set; }
		public U64_Reference<U64_Placeholder> ProhibitedTargetState { get; set; }
		public ushort TargetStatesCount { get; set; }
		public ushort MechanicsIdCardType { get; set; }
		public ushort Repeat { get; set; }
		public ushort Speed { get; set; }
		public byte TransitionStatusFlag { get; set; }
		public byte CustomBits { get; set; }

		public override void SerializeImpl(SerializerObject s) {
			AnimInfo = s.SerializeObject<U64_Reference<U64_Placeholder>>(AnimInfo, name: nameof(AnimInfo));
			MechanicsIdCard = s.SerializeObject<U64_Reference<U64_Placeholder>>(MechanicsIdCard, name: nameof(MechanicsIdCard));
			TargetStateList = s.SerializeObject<U64_Reference<U64_Placeholder>>(TargetStateList, name: nameof(TargetStateList));
			NextState = s.SerializeObject<U64_Reference<GAM_State>>(NextState, name: nameof(NextState));
			ProhibitedTargetState = s.SerializeObject<U64_Reference<U64_Placeholder>>(ProhibitedTargetState, name: nameof(ProhibitedTargetState));

			TargetStatesCount = s.Serialize<ushort>(TargetStatesCount, name: nameof(TargetStatesCount));
			MechanicsIdCardType = s.Serialize<ushort>(MechanicsIdCardType, name: nameof(MechanicsIdCardType));
			Repeat = s.Serialize<ushort>(Repeat, name: nameof(Repeat));
			Speed = s.Serialize<ushort>(Speed, name: nameof(Speed));
			TransitionStatusFlag = s.Serialize<byte>(TransitionStatusFlag, name: nameof(TransitionStatusFlag));
			CustomBits = s.Serialize<byte>(CustomBits, name: nameof(CustomBits));

			NextState?.Resolve(s);
		}
	}
}
