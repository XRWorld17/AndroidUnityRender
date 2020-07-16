package com.huya.nativeandroidapp.activity;

import android.os.Bundle;
import android.widget.FrameLayout;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.view.ViewGroup.LayoutParams;

import com.huya.nativeandroidapp.R;
import com.unity3d.player.UnityPlayer;

public class MainUnitySubview extends AppCompatActivity {

    private UnityPlayer m_UnityPlayer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Create the UnityPlayer
        m_UnityPlayer = new UnityPlayer(this);
        int glesMode = m_UnityPlayer.getSettings().getInt("gles_mode", 1);
        boolean trueColor8888 = false;
        m_UnityPlayer.init(glesMode, trueColor8888);

        setContentView(R.layout.activity_sub);

        FrameLayout layout = (FrameLayout) findViewById(R.id.FrameLayout);
        LayoutParams layoutParams = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT);
        layout.addView(m_UnityPlayer.getView(), 0, layoutParams);

        m_UnityPlayer.windowFocusChanged(true);
        m_UnityPlayer.resume();

    }

    @Override
    public void onWindowFocusChanged(boolean hasFocus)
    {
        super.onWindowFocusChanged(hasFocus);
        m_UnityPlayer.windowFocusChanged(hasFocus);
    }
    @Override
    public void onPause() {
        super.onPause();
        m_UnityPlayer.pause();
    }
    @Override
    public void onResume() {
        super.onResume();
        m_UnityPlayer.resume();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        m_UnityPlayer.quit();
    }
}
