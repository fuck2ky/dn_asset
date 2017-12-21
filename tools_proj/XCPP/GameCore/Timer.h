#ifndef  __Timer__
#define  __Timer__

#include "Common.h"
#include "XTimerMgr.h"
#include "ITimerCallback.h"

class Timer
{
public:
	Timer(int time, int loop, ITimerCallback* handler, uint sequence, ITimerArg* arg);
	void Update(int deltaTime);
	bool IsFinished();
	int GetCurrentTime();
	void Pause();
	void Resume();
	void Reset();
	bool IsSequenceMatched(uint sequence);
	bool IsDelegateMatched(ITimerCallback* handler);


private:
	ITimerCallback* m_handler;
	ITimerArg* m_arg;
	int m_loop;
	int m_totalTime;
	int m_currentTime;
	bool m_isFinished;
	bool m_isRunning;
	uint m_sequence;
};

#endif